-- Database: queststore

-- DROP DATABASE queststore;
DROP DATABASE IF EXISTS queststore;

CREATE DATABASE queststore
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Polish_Poland.1250'
    LC_CTYPE = 'Polish_Poland.1250'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

\c queststore;
/*
DO $$ BEGIN
	CREATE TYPE GENDER AS ENUM
EXCEPTION
	WHEN duplicate_object THEN null;
END $$;

DO $$ BEGIN
	CREATE TYPE CHAL_TYPE AS ENUM ('basic', 'extra');
EXCEPTION
	WHEN duplicate_type THEN null;
END $$;
*/

DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'gender') THEN
        CREATE TYPE gender AS ENUM ('M', 'F');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'que_type') THEN
        CREATE TYPE que_type AS ENUM ('basic', 'extra');
    END IF;
	IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'art_type') THEN
        CREATE TYPE art_type AS ENUM ('basic', 'magic');
    END IF;
	IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'u_role') THEN
        CREATE TYPE u_role AS ENUM ('A', 'C', 'M');
    END IF;
END$$;

CREATE TABLE users (

	user_id SERIAL PRIMARY KEY,
	username VARCHAR(20) UNIQUE NOT NULL,
	password VARCHAR(20),
	email VARCHAR(50) UNIQUE NOT NULL,
	date_of_registration DATE,
	first_name VARCHAR(50) NOT NULL,
	last_name VARCHAR(50) NOT NULL,
	gender GENDER,
	user_role u_role NOT NULL
);

CREATE TABLE mentors (

	mentor_id SERIAL PRIMARY KEY,
	user_id INT REFERENCES users(user_id),
	main_field VARCHAR(40)
);

CREATE TABLE classes (

	class_id SERIAL PRIMARY KEY,

);

CREATE TABLE teams(

	team_id SERIAL PRIMARY KEY,
	class_name VARCHAR(20),
	mentor_id INT REFERENCES mentors(mentor_id),
	class_id INT REFERENCES classes(class_id),
	creation_date DATE
);

CREATE TABLE codecoolers (

	codecooler_id SERIAL PRIMARY KEY,
	team_id INT REFERENCES teams(team_id),
	class_id INT REFERENCES classes(class_id),
	user_id INT REFERENCES users(user_id) UNIQUE NOT NULL,
	coolpoints_wallet INT NOT NULL,
	experience INT NOT NULL
);

CREATE TABLE mentors_classes (

	mentor_id INT REFERENCES mentors(mentor_id),
	class_id INT REFERENCES classes(class_id),
	assignment_date TIMESTAMP,
	PRIMARY KEY (mentor_id, class_id)
);

CREATE TABLE quests (

	quest_id SERIAL PRIMARY KEY,
	quest_name VARCHAR(50) NOT NULL UNIQUE,
	quest_type QUE_TYPE NOT NULL,
	coolpoints_value INT NOT NULL,
	exp_value INT NOT NULL,
	description TEXT NOT NULL
);

CREATE TABLE artifacts (

	artifact_id SERIAL PRIMARY KEY,
	artifact_name VARCHAR(50),
	artifact_type ART_TYPE NOT NULL,
	price INT NOT NULL,
	available_amount INT NOT NULL,
	description TEXT NOT NULL,
	image VARCHAR(50)

);

CREATE TABLE teams_quests (

	team_quest_id SERIAL PRIMARY KEY,
	team_id INT REFERENCES teams(team_id),
	quest_id INT REFERENCES quests(quest_id),
	completion_date TIMESTAMP NOT NULL
);

CREATE TABLE codecoolers_artifacts (

	codecooler_artifact_id SERIAL PRIMARY KEY,
	artifact_id INT REFERENCES artifacts(artifact_id),
	codecooler_id INT REFERENCES codecoolers(codecooler_id),
	transaction_date TIMESTAMP NOT NULL,
	is_used BOOL NOT NULL
);

CREATE TABLE codecoolers_quests (

	codecooler_quest_id SERIAL PRIMARY KEY,
	quest_id INT REFERENCES quests(quest_id),
	codecooler_id INT REFERENCES codecoolers(codecooler_id),
	accepting_mentor_id INT REFERENCES mentors(mentor_id),
	completion_date TIMESTAMP
);

-- Inserting rows into tables

INSERT INTO quests (quest_name, quest_type, coolpoints_value, exp_value, description) VALUES
('Finishing two-week assignment', 'basic', 100, 20, 'Completing each two-week assignment is crucial for stable and fast growth'),
('Passing a checkpoint', 'basic', 250, 40, 'Completing a PA or other kind of knowledge check endures both student and mentors that the method is effective'),
('MR Detective PI', 'extra', 50, 5, 'Spot a major mistake in an assignement'),
('Doing a demo for the class', 'extra', 100, 25, 'Doing a demo for the class (side project, new technology, ...)'),
('Take part in student screening', 'extra', 100, 15, 'Taking part in the student screening process'),
('Workshop for other students', 'extra', 400, 40, 'Organizing a workshop for other students'),
('Whole month on time','extra', 300, 10, 'Attend 1 months without being late'),
('Setting Your own goals!', 'extra', 600, 80, 'Set up a SMART goal accepted by a mentor, then achieve it'),
('Best project of the week', 'extra', 500, 20, 'Students choose the best project of the week. Selected team scores'),
('Meet-up presentation', 'extra', 500, 30, 'Do a presentation on a meet-up. Presentation prepared in team or alone');

INSERT INTO artifacts (artifact_name, artifact_type, price, available_amount, description, image) VALUES
('Private mentoring', 'basic', 50, 10, 'Private mentoring on chosen topic.', 'private-mentoring'),
('Home Office', 'basic', 300, 5, 'You can spend a day in home office', 'home-office'),
('One day SI extra', 'basic', 500, 5, 'Extend SI week assignment deadline by one day', 'si-day'),
('Private workshop', 'magic', 1000, 10, '60 min workshop by a mentor(s) of the chosen topic', 'private-workshop'),
('Acquire mentor', 'magic', 1000, 10, 'Mentor joins a students'' team for a one hour', 'acquire-mentor'),
('Extra material', 'magic', 500, 5, 'Extra material for the current topic', 'extra-material'),
('Mentors dress-up','magic', 5000, 3, 'All mentors should dress up as pirates (or just funny) for the day', 'dress-up'),
('Let''s take it off-school','magic', 30000, 1, 'The whole course goes to an off-school program instead for a day', 'off-sql');

INSERT INTO users (username, password, email, date_of_registration, first_name, last_name, gender,  user_role) VALUES
('drazi', '1234', 'dd@gmail.com', '2020-09-28', 'Dominik', 'd', 'M', 'C'),
('endrde', '1234', 'ad@gmail.com', '2020-09-28', 'Andrzej', 'd', 'M', 'C'),
('admin', 'admin', 'admin@gmail.com', '2020-09-28','AdminFirst', 'AdminLast', 'F', 'A'),
('dsta', '1234', 'ds@gmail.com', '2020-09-28', 'Dominik', 'S', 'M', 'M'),
('kjar', '1234', 'kj@gmail.com', '2020-09-28', 'AKrzysztof', 'J', 'M', 'M');

INSERT INTO mentors (user_id, main_field) VALUES
(4, '.NET'),
(5, '.NET');

INSERT INTO classes (class_name, start_date) VALUES
('fullstack weekend krk', '2020-02-07'),
('fullstack weekend online', '2020-02-07'),
('fullstack daily krk', '2019-10-12');

INSERT INTO teams (mentor_id, class_id, team_name, creation_date) VALUES
(1, 1, 'C of Thieves', '2020-09-26');

INSERT INTO codecoolers (team_id, class_id, user_id, coolpoints_wallet, experience) VALUES
(1, 1, 1, 1000, 120),
(1, 1, 2, 666, 150);






