using System;

namespace Matrizzz
{
    internal class Program
    {
        private static Grafo grafo = new Grafo(false, false);

        static void Main(string[] args)
        {
            bool continuar = true;

            while (continuar)
            {
                MostrarMenu();
                int opcion;

                Console.Write("Seleccione una opción: ");
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    MostrarMensaje("Debe escribir un número válido.");
                    continue;
                }

                Console.WriteLine();

                switch (opcion)
                {
                    case 1:
                        AgregarVertice();
                        break;
                    case 2:
                        AgregarConexion();
                        break;
                    case 3:
                        EliminarConexion();
                        break;
                    case 4:
                        EliminarVertice();
                        break;
                    case 5:
                        grafo.MostrarMatriz();
                        Pausar();
                        break;
                    case 6:
                        EjecutarBFS();
                        break;
                    case 7:
                        EjecutarDFS();
                        break;
                    case 8:
                        continuar = false;
                        Console.WriteLine("Programa finalizado.");
                        break;
                    default:
                        MostrarMensaje("La opción seleccionada no existe.");
                        break;
                }
            }
        }

        private static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("======================================");
            Console.WriteLine("       ADMINISTRADOR DE GRAFOS");
            Console.WriteLine("======================================");
            Console.WriteLine("  1. Registrar vértice");
            Console.WriteLine("  2. Crear conexión");
            Console.WriteLine("  3. Eliminar conexión");
            Console.WriteLine("  4. Eliminar vértice");
            Console.WriteLine("  5. Consultar matriz de adyacencia");
            Console.WriteLine("  6. Recorrido en amplitud (BFS)");
            Console.WriteLine("  7. Recorrido en profundidad (DFS)");
            Console.WriteLine("  8. Salir");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Vértices registrados: " + grafo.CantidadVertices);
            Console.WriteLine();
        }

        private static void AgregarVertice()
        {
            Console.Write("Ingrese el identificador del nuevo vértice: ");
            string entrada = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entrada) || entrada.Trim().Length != 1)
            {
                MostrarMensaje("El identificador debe ser un solo carácter.");
                return;
            }

            grafo.AgregarVertice(new Nodo(entrada.Trim()[0]));
            Pausar();
        }

        private static void AgregarConexion()
        {
            char origen = LeerVertice("Vértice de origen: ");
            char destino = LeerVertice("Vértice de destino: ");

            grafo.AgregarArista(origen, destino);
            Pausar();
        }

        private static void EliminarConexion()
        {
            char origen = LeerVertice("Vértice de origen: ");
            char destino = LeerVertice("Vértice de destino: ");

            grafo.QuitarArista(origen, destino);
            Pausar();
        }

        private static void EliminarVertice()
        {
            char identificador = LeerVertice("Identificador del vértice que desea eliminar: ");
            grafo.QuitarVertice(identificador);
            Pausar();
        }

        private static void EjecutarBFS()
        {
            char inicio = LeerVertice("Vértice inicial para BFS: ");
            grafo.RecorridoAmplitud(inicio);
            Pausar();
        }

        private static void EjecutarDFS()
        {
            char inicio = LeerVertice("Vértice inicial para DFS: ");
            grafo.RecorridoProfundidad(inicio);
            Pausar();
        }

        private static char LeerVertice(string mensaje)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(entrada) || entrada.Trim().Length != 1)
            {
                Console.Write("Escriba solamente un carácter: ");
                entrada = Console.ReadLine();
            }

            return entrada.Trim()[0];
        }

        private static void MostrarMensaje(string mensaje)
        {
            Console.WriteLine();
            Console.WriteLine(mensaje);
            Pausar();
        }

        private static void Pausar()
        {
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
