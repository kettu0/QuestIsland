using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStore_C_of_thieves.DAO
{
    public class ConnectionDAO
    {
        internal string conn =
            Environment.MachineName == "GLMC005"
                ? "Server=localhost; Username=postgres; Password='a1b2c3d4'; Database=queststore"
                : Environment.MachineName == "LAPTOP-326KG9K9"
                    ? "Server=localhost; Username='Shaman'; Password='Codecool@123'; Database=queststore"
                    : Environment.MachineName == "LAPTOP-90SUHSSD"
                        ? "Server=localhost; Username=aneta; Password='Roy666'; Database=queststore"
                        : Environment.MachineName == "DESKTOP-LJBN8VS"
                            ? "Server=localhost; Username=postgres; Password='690847321'; Database=queststore"
                            : Environment.MachineName == "DESKTOP-IS86UA2"
                                ? "Server=localhost; Username=postgres; Password='hydra'; Database=queststore"
                                : null;
    }
}
