using System;


class MainClass
{

	//Estructura para guardra la informacion del Alumno
	public class AlumnoInfo
	{

		//Nombre del Alumno
		public string Nombre;


		//Lista de materias del alumno
		public System.Collections.ArrayList Materias = new System.Collections.ArrayList();

			
		public double Promedio {
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



		public void CapturarNombre ()
		{
			Console.Write ("Nombre del Alumno : ");
			this.Nombre = Console.ReadLine ();
		}

		public void CapturarCalificaciones ()
		{
			//Capturar cada una de las materias agregadas en la lisya
			foreach (Materia iMateria in this.Materias) {
				iMateria.Capturar ();
			}
		}

		public void MostrarCalificaciones ()
		{

			Console.WriteLine ("\n\n" + this.Nombre);
			Console.WriteLine ("".PadRight (30, '-'));
			Console.WriteLine (String.Format ("{0,-15}| {1,-15}", "Materia", "Calificacion"));
			Console.WriteLine ("".PadRight (30, '-'));
			foreach (Materia iMateria in this.Materias) {

				Console.WriteLine (String.Format ("{0,-15}| {1,-15}", iMateria.Descripcion.PadRight (15, '.'), iMateria.Calificacion));

			}
			Console.WriteLine ("".PadRight (30, '-'));
			Console.WriteLine (String.Format ("{0,-15}{1,-15}", "Promedio".PadRight (15, '.'), this.Promedio.ToString ("0.00")));

		}
			
	}

	public class Materia
	{
//Estructura para guardar la informacion de la materia

		public string Descripcion;
		public double Calificacion;

		public Materia (string Mat)
		{
			this.Descripcion = Mat;
			this.Calificacion = 0;
		}

		public void Capturar ()
		{//Capturar calificacion de la materia
			Console.Write ("- {0,-15} : ", this.Descripcion);
			this.Calificacion = double.Parse (Console.ReadLine ());
		}

	}

	public static void Main (string[] args)
	{
		System.Collections.ArrayList Alumnos = new System.Collections.ArrayList ();
		System.Collections.ArrayList LstMaterias = new System.Collections.ArrayList ();


		for (int iContador = 1; iContador < 5; iContador++) {

			AlumnoInfo iAlumno = new AlumnoInfo ();

			iAlumno.CapturarNombre ();//Captuar nombre

			//Materias a calificar en los alumnos
			iAlumno.Materias.Add (new Materia ("Espanol"));
			iAlumno.Materias.Add (new Materia ("Historia"));
			iAlumno.Materias.Add (new Materia ("Matematicas"));

			iAlumno.CapturarCalificaciones ();//Capturar la lista de Calificaciones
			Alumnos.Add (iAlumno);//Agregar alumno a la lista

		}

		Console.WriteLine ("\n\nCalificaciones de Alumnos");
		foreach (AlumnoInfo iAlumno in Alumnos) {
			iAlumno.MostrarCalificaciones ();

			if (iAlumno.Promedio > 6) {
				Console.WriteLine ("Aprobado :D");
			} else {
				Console.WriteLine ("Reprobado :(");
			}

		}



	}



}

