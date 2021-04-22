using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace Final.Pages
{
    public class Alumnos{
        public Guid Id;
        public string Nombre;
        public int Cuenta;
        public double Promedio;
    }
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Alumnos> Alumno = new List<Alumnos>();
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            SqlConnection con = new SqlConnection(@"Data Source=unitec-db.database.windows.net;
                                                    Initial Catalog=clases;
                                                    User ID=loginAlumno;
                                                    Password=Pa$$word;");

            con.Open();

            SqlCommand cmd = new SqlCommand ("SELECT [Id],[Nombre],[Cuenta],[Promedio] FROM [ad_Alumnos];", con);

            SqlDataReader rdr = cmd.ExecuteReader();
            
            while(rdr.Read())
            {
                Alumno.Add(
                    new Alumnos{
                        Id = (Guid)rdr["Id"],
                        Nombre = (string)rdr["Nombre"],
                        Cuenta = (int)rdr["Cuenta"],
                        Promedio = (double)rdr["Promedio"],
                 });
            }

            con.Close();
           
        }
    }
}
