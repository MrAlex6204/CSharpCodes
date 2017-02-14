using System;

class RecursosHumanos
{

	//Esta clase guarda la informacion de cada empleado capturado
	public class EmpleadoInfo{

		public string Nombre,Puesto;//Nombre y puesto del Empleado
		public double SueldoMensual;//Sueldo mensual
		public int Faltas;//Faltas del Empleado
		public int Retardos;//Retardos 

		public double ObtenerSalarioDiario(){//Obtiene el salario diario
			return this.SueldoMensual/30;
		}

		public double ObtenerSueldo(int NumDeFaltasPorRetardos){
			//Calcula y obtiene el Sueldo del Empleado
			double Sueldo = 0;

			//Decontarle al sueldo mensual las faltas  y los retardos
			Sueldo = this.SueldoMensual - (this.DescuentoPorFaltas () + this.DescuentoPorRetardos (NumDeFaltasPorRetardos));

			return Sueldo;		
		}

		public double DescuentoPorFaltas(){//Retorna la cantidad a descontar por numero de faltas
			
			double Descuento = this.ObtenerSalarioDiario() * this.Faltas;

			return Descuento;
		}

		public double DescuentoPorRetardos(int NumDeFaltasPorRetardos){
			//Retorna la cantidad a descontar por la cantidad de retardos acumulados
			
			double Descuento = 0;
			int FaltasPorRetardos = this.ObtenerCantidadFaltasPorRetardos(NumDeFaltasPorRetardos);
			
			Descuento = (FaltasPorRetardos * this.ObtenerSalarioDiario ());
			
			return Descuento;
		}

		public int ObtenerCantidadFaltasPorRetardos(int NumDeFaltasPorRetardos){//Calcular las faltas x retardos y obtener el valor entero
			
			int FaltasPorRetardos =0;

			if (NumDeFaltasPorRetardos > 0) {
				//Calcular solo en caso de que el valorNumDeRetardosPorFaltas  sea mayor a cero				
				FaltasPorRetardos = (int)(this.Retardos/NumDeFaltasPorRetardos);							
			}

			return FaltasPorRetardos;
		}



		public string MostrarDescripcionPorFaltas(){
			//Retorna una descripcion del descuento por Faltas

			if (this.Faltas > 0) {
				//Mostrar la cant. de faltas y el descuento de Faltas x SalarioDiario
				return String.Format("{0} ({1})",this.Faltas,this.DescuentoPorFaltas().ToString("$ 0.00 Pesos"));				
			} else {
				//Mostrar 0 faltas
				return String.Format("0",this.Faltas);				
			}
		}

		public string MostrarDescripcionPorRetardos(int NumDeFaltasPorRetardos){
			//Retorna una descripcion del descuento por Retardos
			int FaltasPorRetardos =  this.ObtenerCantidadFaltasPorRetardos(NumDeFaltasPorRetardos);

			if (FaltasPorRetardos > 0) {
				//Mostrar la cant. de faltas y el descuento de Faltas x SalarioDiario
				return String.Format("{0} ({1})",this.Retardos,this.DescuentoPorRetardos(NumDeFaltasPorRetardos).ToString("$ 0.00 Pesos"));				
			} else {
				//Mostrar 0 retardos
				return String.Format("0",this.Faltas);				
			}
		}

		public void MostrarQuincena(int NumDeFaltasPorRetardos){

			Console.WriteLine("{0}",this.Nombre);

			Console.WriteLine ("{0,-20}{1,-20}","".PadRight(20,'-'), "".PadRight(20,'-'));

			Console.WriteLine ("{0,-20} {1,-20}","Puesto".PadRight(20,'.'), this.Puesto);
			Console.WriteLine ("{0,-20} {1,-20}","Salario Mensual".PadRight(20,'.'), this.SueldoMensual.ToString("$ 0.00 Pesos"));
			Console.WriteLine ("{0,-20} {1,-20}","Faltas".PadRight(20,'.'), this.MostrarDescripcionPorFaltas());
			Console.WriteLine ("{0,-20} {1,-20}","Retardos".PadRight(20,'.'), this.MostrarDescripcionPorRetardos(NumDeFaltasPorRetardos));

			Console.WriteLine ("{0,-20}{1,-20}","".PadRight(20,'-'), "".PadRight(20,'-'));

			Console.WriteLine ("{0,-20} {1,-20}","Sueldo".PadRight(20,'.'), this.ObtenerSueldo(NumDeFaltasPorRetardos).ToString("$ 0.00 Pesos"));

		}
	}

	public static void Main (string[] args)
	{
		int CantidadDeEmpleados = 20;//Cantidad de empleados a capturar
		int CantRetardosPorFalta = 3; //Indicar la cantidad de retardos por faltas
		int IndexNo = 0;
		EmpleadoInfo[] ListaDeEmpleados = new  EmpleadoInfo[CantidadDeEmpleados];


		while (IndexNo < CantidadDeEmpleados) {

			EmpleadoInfo iEmpleado = new EmpleadoInfo ();

			Console.WriteLine ("--------------------------------------");
			Console.WriteLine ("Teclee el Nombre del Empleado :");
			iEmpleado.Nombre = Console.ReadLine ();

			Console.WriteLine ("Teclee el Puesto del Empleado :");
			iEmpleado.Puesto = Console.ReadLine ();

			Console.WriteLine ("Teclee el Sueldo Mensual del Empleado :");
			iEmpleado.SueldoMensual = double.Parse( Console.ReadLine ());

			Console.WriteLine ("Teclee la cant. de Faltas  :");
			iEmpleado.Faltas = Int16.Parse( Console.ReadLine ());

			Console.WriteLine ("Teclee la cant. de Retardos  :");
			iEmpleado.Retardos = Int16.Parse( Console.ReadLine ());

			Console.WriteLine ("--------------------------------------");

			ListaDeEmpleados [IndexNo] = iEmpleado;

			IndexNo++;//Incrementar la posicion
		}
		Console.WriteLine ("\n\nPresione cualquier tecla para ir mostrando la lista de Empleados\n\n");
		IndexNo = 0;//Desplazarse a la posicion 0

		do {

			ListaDeEmpleados[IndexNo].MostrarQuincena(CantRetardosPorFalta);//Mostrar la informacion de la nomina del empleado
			Console.ReadLine ();
			IndexNo++;
		} while(IndexNo < CantidadDeEmpleados);



		Console.WriteLine ("\n\nPresione cualquier tecla para salir");
		Console.ReadLine ();
	}
}

