using System;
using System.Collections.Generic;

namespace Matrizzz
{
    internal class Grafo
    {
        private List<Nodo> vertices;
        private int[,] conexiones;
        private bool esDirigido;
        private bool esPonderado;

        public int CantidadVertices
        {
            get { return vertices.Count; }
        }

        public Grafo(bool dirigido, bool ponderado)
        {
            vertices = new List<Nodo>();
            conexiones = new int[0, 0];
            esDirigido = dirigido;
            esPonderado = ponderado;
        }

        public void AgregarVertice(Nodo nuevo)
        {
            if (BuscarIndice(nuevo.Identificador) != -1)
            {
                Console.WriteLine("No se agregó: ese vértice ya está registrado.");
                return;
            }

            vertices.Add(nuevo);

            int cantidad = vertices.Count;
            int[,] matrizNueva = new int[cantidad, cantidad];

            for (int fila = 0; fila < cantidad - 1; fila++)
            {
                for (int columna = 0; columna < cantidad - 1; columna++)
                {
                    matrizNueva[fila, columna] = conexiones[fila, columna];
                }
            }

            conexiones = matrizNueva;
            Console.WriteLine("Vértice agregado correctamente.");
        }

        public void AgregarArista(char origen, char destino, int peso = 1)
        {
            int fila = BuscarIndice(origen);
            int columna = BuscarIndice(destino);

            if (!IndicesValidos(fila, columna))
                return;

            if (conexiones[fila, columna] != 0)
            {
                Console.WriteLine("Ya existe una conexión entre esos vértices.");
                return;
            }

            int valor = esPonderado ? peso : 1;
            conexiones[fila, columna] = valor;

            if (!esDirigido)
                conexiones[columna, fila] = valor;

            Console.WriteLine("Conexión creada correctamente.");
        }

        public void QuitarArista(char origen, char destino)
        {
            int fila = BuscarIndice(origen);
            int columna = BuscarIndice(destino);

            if (!IndicesValidos(fila, columna))
                return;

            if (conexiones[fila, columna] == 0)
            {
                Console.WriteLine("No existe una conexión entre esos vértices.");
                return;
            }

            conexiones[fila, columna] = 0;

            if (!esDirigido)
                conexiones[columna, fila] = 0;

            Console.WriteLine("Conexión eliminada correctamente.");
        }

        public void QuitarVertice(char identificador)
        {
            int indice = BuscarIndice(identificador);

            if (indice == -1)
            {
                Console.WriteLine("No se encontró el vértice indicado.");
                return;
            }

            vertices.RemoveAt(indice);

            int nuevoTamano = vertices.Count;
            int[,] matrizNueva = new int[nuevoTamano, nuevoTamano];

            int nuevaFila = 0;
            for (int fila = 0; fila < conexiones.GetLength(0); fila++)
            {
                if (fila == indice)
                    continue;

                int nuevaColumna = 0;
                for (int columna = 0; columna < conexiones.GetLength(1); columna++)
                {
                    if (columna == indice)
                        continue;

                    matrizNueva[nuevaFila, nuevaColumna] = conexiones[fila, columna];
                    nuevaColumna++;
                }

                nuevaFila++;
            }

            conexiones = matrizNueva;
            Console.WriteLine("Vértice y sus conexiones eliminados correctamente.");
        }

        public void MostrarMatriz()
        {
            Console.WriteLine("========== MATRIZ DE ADYACENCIA ==========");

            if (vertices.Count == 0)
            {
                Console.WriteLine("Todavía no hay vértices registrados.");
                return;
            }

            Console.Write("    ");
            foreach (Nodo vertice in vertices)
                Console.Write("{0,4}", vertice.Identificador);

            Console.WriteLine();

            for (int fila = 0; fila < vertices.Count; fila++)
            {
                Console.Write("{0,3} ", vertices[fila].Identificador);

                for (int columna = 0; columna < vertices.Count; columna++)
                    Console.Write("{0,4}", conexiones[fila, columna]);

                Console.WriteLine();
            }
        }

        public void RecorridoAmplitud(char inicio)
        {
            int indiceInicio = BuscarIndice(inicio);

            if (indiceInicio == -1)
            {
                Console.WriteLine("El vértice inicial no existe.");
                return;
            }

            bool[] visitados = new bool[vertices.Count];
            Queue<int> pendientes = new Queue<int>();

            visitados[indiceInicio] = true;
            pendientes.Enqueue(indiceInicio);

            Console.Write("Resultado BFS: ");

            while (pendientes.Count > 0)
            {
                int actual = pendientes.Dequeue();
                Console.Write(vertices[actual].Identificador + " ");

                for (int i = 0; i < vertices.Count; i++)
                {
                    if (conexiones[actual, i] != 0 && !visitados[i])
                    {
                        visitados[i] = true;
                        pendientes.Enqueue(i);
                    }
                }
            }

            Console.WriteLine();
        }

        public void RecorridoProfundidad(char inicio)
        {
            int indiceInicio = BuscarIndice(inicio);

            if (indiceInicio == -1)
            {
                Console.WriteLine("El vértice inicial no existe.");
                return;
            }

            bool[] visitados = new bool[vertices.Count];

            Console.Write("Resultado DFS: ");
            ExplorarProfundidad(indiceInicio, visitados);
            Console.WriteLine();
        }

        private void ExplorarProfundidad(int actual, bool[] visitados)
        {
            visitados[actual] = true;
            Console.Write(vertices[actual].Identificador + " ");

            for (int siguiente = 0; siguiente < vertices.Count; siguiente++)
            {
                if (conexiones[actual, siguiente] != 0 && !visitados[siguiente])
                    ExplorarProfundidad(siguiente, visitados);
            }
        }

        private int BuscarIndice(char identificador)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (char.ToUpper(vertices[i].Identificador) == char.ToUpper(identificador))
                    return i;
            }

            return -1;
        }

        private bool IndicesValidos(int fila, int columna)
        {
            if (fila == -1 || columna == -1)
            {
                Console.WriteLine("No se pudo realizar la operación: verifica que ambos vértices existan.");
                return false;
            }

            return true;
        }
    }
}
