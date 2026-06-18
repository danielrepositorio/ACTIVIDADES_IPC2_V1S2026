namespace ControlAcademicoMvc.Models
{
    
    public static class MemoriaEstudiantes
    {
        private static Estudiante[] estudiantes = new Estudiante[20];
        private static int contador = 0;

        static MemoriaEstudiantes()
        {
            Registrar(new Estudiante { Carne = 2026012, Nombre = "Fernando Velásquez", Promedio = 91.5 });
            Registrar(new Estudiante { Carne = 2026045, Nombre = "María Mercedes", Promedio = 84.0 });
        }

        public static bool Registrar(Estudiante nuevo)
        {
            if (contador >= estudiantes.Length)
            {
                return false;
            }

            estudiantes[contador] = nuevo;
            contador++;
            return true;
        }

        public static Estudiante[] ObtenerTodos()
        {
            Estudiante[] copia = new Estudiante[contador];

            for (int i = 0; i < contador; i++)
            {
                copia[i] = estudiantes[i];
            }

            return copia;
        }

        public static Estudiante? BuscarPorCarne(int carne)
        {
            for (int i = 0; i < contador; i++)
            {
                if (estudiantes[i].Carne == carne)
                {
                    return estudiantes[i];
                }
            }

            return null;
        }
    }
}