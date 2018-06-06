using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carona_Service.Enums
{
    public static class Dicionarios
    {
        public static Dictionary<string, EnumSexo> DicionarioSexo = new Dictionary<string, EnumSexo>{
            {"male", EnumSexo.MASCULINO },
            {"female", EnumSexo.FEMININO }
        };
    }
}
