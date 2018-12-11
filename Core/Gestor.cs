using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Gestor
    {
        private static Gestor Instancia;
        private Grafo<Lugar> Grafo;
        public List<City> cosas;
        public Distance[,] distances;
        private Gestor()
        {
            Grafo = new Grafo<Lugar>(false);
            this.cosas = new List<City>();
            this.distances = new Distance[9999, 9999];
        }

        public static Gestor GetInstancia()
        {
            if (Instancia == null)
            {
                Instancia = new Gestor();
            }
            return Instancia;
        }

        public bool InsertarVertice(string pNombre, double pLongitud, double pLatitud)
        {
            return Grafo.InsertaVertice(pNombre, new Lugar(pNombre, pLatitud, pLongitud));
        }
        public bool InsertarArco(string pNombreVerticeA, string pNombreVerticeB, double pPeso)
        {
            return Grafo.InsertarArco(pNombreVerticeA, pNombreVerticeB, pPeso);
        }
        public Lista<Lugar> GetCaminoMasRapido(string pNombreVerticeA, string pNombreVerticeB)
        {

            return null;
        }
        public List<Lugar> GetRutaMinimaDijkstra(string pNombreVerticeA, string pNombreVerticeB)
        {
            Cola<Vertice<Lugar>> ColaCaminoMinimo = Grafo.RutaMinimaDijkstra(pNombreVerticeA, pNombreVerticeB);
            List<Lugar> ListaLugares = new List<Lugar>();
            Iterador<Vertice<Lugar>> iterador = new Iterador<Vertice<Lugar>>(ColaCaminoMinimo.GetInicio());
            for (Vertice<Lugar> verticeAdyac = iterador.Next(); verticeAdyac != null; verticeAdyac = iterador.Next())
            {
                ListaLugares.Add(verticeAdyac.Info);
            }
            return ListaLugares;
        }

        public ReturnValue dijkstra(string city1, string city2)
        {
            int start = this.cosas.FindIndex(c => c.Name == city1);
            int end = this.cosas.FindIndex(c => c.Name == city2);
            this.CalculateDistancesbetweennodes(city1, city2);
            var nodeCount = this.cosas.Count;
            var infinity = int.MaxValue;
            var shortestPath = new double[nodeCount];
            var nodeChecked = new bool[nodeCount];
            var pred = new int[nodeCount];
            double totalDistance;

            for (var i = 0; i < nodeCount; i++)
            {
                shortestPath[i] = infinity;
                pred[i] = -1;
                nodeChecked[i] = false;
            }

            shortestPath[start] = 0;

            for (var i = 0; i < nodeCount; i++)
            {

                double minDist = infinity;
                int? closestNode = null;

                for (var j = 0; j < nodeCount; j++)
                {

                    if (!nodeChecked[j])
                    {
                        if (shortestPath[j] <= minDist)
                        {
                            minDist = shortestPath[j];
                            closestNode = j;
                        }
                    }
                }

                nodeChecked[closestNode.Value] = true;

                for (var k = 0; k < nodeCount; k++)
                {
                    if (!nodeChecked[k])
                    {
                        var nextDistance = DistanceBetween(closestNode.Value, k, this.distances);
                        if ((shortestPath[closestNode.Value] + Convert.ToDouble(nextDistance.Value)) < shortestPath[k])
                        {
                            var soFar = shortestPath[closestNode.Value];
                            var extra = Convert.ToDouble(nextDistance.Value);
                            shortestPath[k] = Convert.ToDouble(soFar) + extra;
                            pred[k] = closestNode.Value;
                        }
                    }
                }

                if (shortestPath[end] < infinity)
                {
                    var newPath = new List<Step>();
                    var step = new Step() { Target = Convert.ToInt32(end) };

                    var v = Convert.ToInt32(end);

                    while (v >= 0)
                    {
                        v = pred[v];
                        if (v != -1 && v >= 0)
                        {
                            step.Source = v;
                            newPath.Insert(newPath.Count == 0 ? 0 : newPath.Count - 1, step);
                            step = new Step() { Target = Convert.ToInt32(v) };
                        }
                    }

                    totalDistance = shortestPath[end];

                    return new ReturnValue()
                    {
                        Mesg = "Status: OK",
                        Path = newPath,
                        Source = start,
                        Target = end,
                        Distance = totalDistance
                    };
                }
                else
                {
                    return new ReturnValue()
                    {
                        Mesg = "Sorry No path found",
                        Path = null,
                        Source = start,
                        Target = end,
                        Distance = 0
                    };
                }
            }

            return null;
        }

        private void CalculateDistancesbetweennodes(string city1, string city2)
        {
            for (var i = 0; i < this.cosas.Count; i++)
            {
                for (var j = 0; j < this.cosas.Count; j++)
                {
                    this.distances[i, j] = new Distance() { Value = "x" };
                }
            }

            foreach(var city in this.cosas)
            {
                var sourceNodeId = 0;// parseInt(mapdata.paths[i].from);
                var targetNodeId = 3;// parseInt(mapdata.paths[i].to);
                var sourceNode = city.LatLon;
                var targetNode = this.cosas.First(c => c.Name == city2).LatLon;
                var p1 = sourceNode;
                var p2 = targetNode;
                var d = p1.DistanceTo(p2);
                this.distances[sourceNodeId, targetNodeId].Value = d.ToString();
                this.distances[targetNodeId, sourceNodeId].Value = d.ToString();
            }
            //var sourceNodeId = 0;// parseInt(mapdata.paths[i].from);
            //var targetNodeId = 3;// parseInt(mapdata.paths[i].to);
            //var sourceNode = this.cosas.First(c => c.Name == city1).LatLon;
            //var targetNode = this.cosas.First(c => c.Name == city2).LatLon;
            //var p1 = sourceNode;
            //var p2 = targetNode;
            //var d = p1.DistanceTo(p2);
            //this.distances[sourceNodeId, targetNodeId].Value = d.ToString();
            //this.distances[targetNodeId, sourceNodeId].Value = d.ToString();
        }

        private Distance DistanceBetween(int fromNode, int toNode, Distance[,] distances)
        {
            var dist = distances[fromNode, toNode];
            if (dist == null || dist.Value == "x")
            {
                dist = new Distance() { Value = int.MaxValue.ToString() };
            }

            return dist;
        }

        public class Step
        {
            public int Target { get; set; }
            public int Source { get; set; }
        }

        public class Distance
        {
            public string Value { get; set; }
        }

        public class ReturnValue
        {
            public string Mesg { get; set; }
            public List<Step> Path { get; set; }
            public int Source { get; set; }
            public int Target { get; set; }
            public double Distance { get; set; }
        }

        public class City
        {
            public City(string name, LatLon latLon)
            {
                this.Name = name;
                this.LatLon = latLon;
            }

            public string Name { get; set; }
            public LatLon LatLon { get; set; }
        }

        public class LatLon
        {
            public LatLon(double x, double y)
            {
                this.X = x;
                this.Y = y;
            }

            public double X { get; set; }
            public double Y { get; set; }

            public double ToRadians(double number)
            {
                return number * Math.PI / 180;
            }

            public double ToDegrees(double number)
            {
                return number * 180 / Math.PI;
            }

            public double DistanceTo(LatLon p2)
            {
                var radius = 6378137;
                var R = radius;
                var a1 = this.ToRadians(this.X);
                var z1 = this.ToRadians(this.Y);
                var b1 = p2.ToRadians(p2.X);
                var y2 = p2.ToRadians(p2.Y);
                var Δφ = b1 - a1;
                var Δλ = y2 - z1;
                var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2)
                    + Math.Cos(a1) * Math.Cos(b1)
                    * Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
                var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                var d = R * c;
                return d;
            }
        }
    }
}
