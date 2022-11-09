using System;
using System.Text.Json;
using CoderHouse_SistemaGestion.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoderHouse_SistemaGestion.Repository
{
    public class SistemaRepository
    {



        public static String GetNombreSistema()
        {
            Sistema sistem = new Sistema();
            var sistemaJson = JsonSerializer.Serialize(sistem);
            return  sistemaJson;

        }


    }
}
