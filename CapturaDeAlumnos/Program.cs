using System;


	class MainClass
	{

		public class AlumnoInfo
		{//Estructura para guardra la informacion del Alumno

			public string Nombre;//Nombre del Alumno

			public System.Collections.ArrayList Materias;//Lista de materias del alumno

			
			public double Promedio {//Calcula el promedio de todas las materias agregadas en la lista
				get { 
					double CalificacionSumatoria = 0;
					double Prom = 0;
					int TotalMaterias = this.Materias.Count;
					
					foreach (Materia iMateria in this.Materias) {
						CalificacionSumatoria += iMateria.Calificacion;//Realizamos la suma de todas las materias
					}
					
					Prom = CalificacionSumatoria / TotalMaterias;
					
					
					return Prom;//Retornamos el promedio calculado
				}
			}



			public void CapturarNombre(){
				Console.Write("Nombre del Alumno : ");
				this.Nombre = Console.ReadLine();
			}

			public void CapturarCalificaciones(){
				//Capturar cada una de las materias agregadas en la lisya
				foreach(Materia iMateria in this.Materias){
					iMateria.Capturar();
				}
			}

			public void MostrarCalificaciones(){

				Console.WriteLine("\n\n"+this.Nombre);
				Console.WriteLine("".PadRight(30,'-'));
				Console.WriteLine (String.Format ("{0,-15}| {1,-15}", "Materia", "Calificacion"));
				Console.WriteLine("".PadRight(30,'-'));
				foreach(Materia iMateria in this.Materias){

					Console.WriteLine (String.Format ("{0,-15}| {1,-15}", iMateria.Descripcion.PadRight(15,'.'), iMateria.Calificacion));

				}
				Console.WriteLine("".PadRight(30,'-'));
				Console.WriteLine (String.Format ("{0,-15}{1,-15}", "Promedio".PadRight(15,'.'), this.Promedio.ToString("0.00")));

			}
			
		}

		public class Materia
		{//Estructura para guardar la informacion de la materia

			public string Descripcion;
			public double Calificacion;

			public Materia (string Mat)
			{
				this.Descripcion = Mat;
				this.Calificacion = 0;
			}

			public void Capturar(){//Capturar calificacion de la materia
				Console.Write("- {0,-15} : ",this.Descripcion);
				this.Calificacion = double.Parse(Console.ReadLine());
			}

		}

		public static void Main (string[] args)
		{
			System.Collections.ArrayList Alumnos = new System.Collections.ArrayList ();
			System.Collections.ArrayList LstMaterias = new System.Collections.ArrayList ();

			//Materias a calificar en los alumnos
			LstMaterias.Add (new Materia ("Espanol"));
			LstMaterias.Add (new Materia ("Historia"));
			LstMaterias.Add (new Materia ("Matematicas"));


			for (int iContador = 1; iContador < 5; iContador++) {//Capturar solo 5 Alumnos

				AlumnoInfo iAlumno = new AlumnoInfo ();//Crear una instancia de Alumno

				iAlumno.CapturarNombre ();//Captuar nombre del alumno
				iAlumno.Materias = LstMaterias;//Indicar la lista de materias a capturar
				iAlumno.CapturarCalificaciones();//Capturar la lista de Calificaciones
				Alumnos.Add (iAlumno);//Agregar alumno a la lista

			}

			Console.WriteLine ("\n\nCalificaciones de Alumnos");
			foreach (AlumnoInfo iAlumno in Alumnos) {
				iAlumno.MostrarCalificaciones ();//Mostrar las calificaciones de cada alumno
			}



		}



	}

