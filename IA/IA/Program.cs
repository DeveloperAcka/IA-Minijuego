using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA
{
    class Program
    {
        static void Main(string[] args)
        {
            //creación del objeto de interacción
            Program P = new Program();
            P.iniciar();
        }

        //función de inicialización del juego
        public void iniciar()
        {
            //carga de cartas
            carta[] stock = cargarCartas();

            //creamos el mazo del usuario y lo llenamos
            carta[] usuario = deckUsuario(stock);

            //creamos el mazo de la máquina y lo llenamos
            carta[] maquina = deckMaquina(stock);

            //imprimir el mazo del jugador
            imprimirMazo(usuario, "USUARIO ");

            //imprimir el mazo de la maquina
            imprimirMazo(maquina, "MAQUINA ");

            //consola
            Console.WriteLine("\nInicia el juego");
            Console.ReadKey();
            Console.Clear();

            //Pregunta al jugador si quiere cara o sello
            String deseoJugador;
            Console.WriteLine("Pregunta para el usuario: Desea cara o sello?");
            deseoJugador = Console.ReadLine();

            //variables de orden de turnos
            String primerJugador = " ";
            String segundoJugador = " ";

            //Función que contiene algoritmo para asignar mediante moneda los turnos de los jugadores
            String[] orden = caraSello(deseoJugador);
            primerJugador = orden[0];
            segundoJugador = orden[1];

            Console.ReadKey();

            //Iniciar el juego
            juego(primerJugador, segundoJugador, usuario, maquina);

            //Cerrar el juego
            Environment.Exit(0);
        }

        //función del juego completo
        public void juego(String primerJugador, String segundoJugador, carta[] usuario, carta[] maquina)
        {
            //impresión de comienzo de juego
            Console.Clear();
            Console.WriteLine("A jugar");
            Console.ReadKey();
            
            //comienzan los duelos - inicializar contadores de puntos por jugador
            int puntosUsuario = 0;
            int puntosMaquina = 0;
            int contador = 0;   //contador de  duelos
            Console.Clear();

            while (puntosUsuario < 3 && puntosMaquina < 3) //mientras alguno de los 2 llegue a los 3 puntos
            {
                imprimirRonda(contador); //imprime la ronda del juego

                if (contador%2 == 0)
                {
                    //turno par
                    imprimirEntrada(primerJugador);

                    if (primerJugador == "maquina")
                    {
                        //***********************************************************************************************************************************************************
                        //propuesta Randómica de la maquina
                        String prioridad = "MAQUINA";
                        int opc = generarPropuestaMaquina(); //selecciona cuál es el criterio de victoria de la ronda
                        imprimirElección("MAQUINA", opc);
                        int movi = seleccionarCartaMaquina(maquina);
                        maquina = adicionales(maquina, movi, "MAQUINA", opc); //función para aplicar adicionales
                        imprimirEleccionCarta("MAQUINA", maquina[movi]); //IMPRESIÓN DE LA CARTA FINAL FINAL A JUGAR

                        //guardo en un objeto llamado seleccion usuario, el objeto antes de borrarlo para compararlo
                        carta SelMa = maquina[movi];

                        //eliminar la carta del mazo 
                        maquina[movi] = null;
                        Console.ReadKey();
                        //***********************************************************************************************************************************************************


                        //***********************************************************************************************************************************************************
                        //respuesta del usuario
                        Console.WriteLine("\nTURNO DEL USUARIO");
                        Console.ReadKey();
                        movi = seleccionarCartaUsuario(usuario);
                        usuario = adicionales(usuario, movi, "USUARIO", opc);
                        imprimirEleccionCarta("USUARIO", usuario[movi]);

                        //guardo en un objeto llamado seleccion usuario, el objeto antes de borrarlo para compararlo
                        carta SelUs = usuario[movi];

                        //eliminar la carta del mazo 
                        usuario[movi] = null;
                        //***********************************************************************************************************************************************************
 

                        //***********************************************************************************************************************************************************
                        //COMPARACIÓN DE QUIÉN GANA
                        //genero un arreglo con lo recibido por el metodo que decide el ganador del duelo
                        int[] respuesta = ganadorDuelo(opc, SelMa, SelUs, puntosMaquina, puntosUsuario, prioridad);
                        puntosUsuario = respuesta[0];
                        puntosMaquina = respuesta[1];
                        //***********************************************************************************************************************************************************
                    }
                    else
                    {
                        //***********************************************************************************************************************************************************
                        //PROPUESTA DEL USUARIO
                        String prioridad = "USUARIO";
                        int opc = mostrarEleccionUsuario();
                        Console.ReadLine();
                        imprimirElección("USUARIO", opc);
                        int movi = 0;
                        movi = seleccionarCartaUsuario(usuario);
                        usuario = adicionales(usuario, movi, "USUARIO", opc);
                        imprimirEleccionCarta("USUARIO", usuario[movi]);

                        //guardo en un objeto llamado seleccion usuario, el objeto antes de borrarlo para compararlo
                        carta SelUs = usuario[movi];

                        //eliminar la carta del mazo 
                        usuario[movi] = null;
                        //***********************************************************************************************************************************************************


                        //***********************************************************************************************************************************************************
                        //respuesta de la máquina
                        Console.WriteLine("\nTURNO DE LA MÁQUINA");
                        Console.ReadKey();
                        bool selec = false;
                        movi = seleccionarCartaMaquina(maquina);
                        maquina = adicionales(maquina, movi, "MAQUINA", opc); //función para aplicar adicionales
                        imprimirEleccionCarta("MAQUINA", maquina[movi]);

                        //guardo en un objeto llamado seleccion de la maquina, el objeto antes de borrarlo para compararlo
                        carta SelMa = maquina[movi];

                        //eliminar la carta del mazo 
                        maquina[movi] = null;
                        //***********************************************************************************************************************************************************


                        //***********************************************************************************************************************************************************
                        //COMPARACIÓN DE QUIÉN GANA
                        //genero un arreglo con lo recibido por el metodo que decide el ganador del duelo
                        int[] respuesta = ganadorDuelo(opc, SelMa, SelUs, puntosMaquina, puntosUsuario, prioridad);
                        puntosUsuario = respuesta[0];
                        puntosMaquina = respuesta[1];
                        //************************************************************************************************************************************************************
                    }

                }

              if (contador%2 == 1)
                {
                    Console.ReadKey();
                    Console.Clear();
                    //turno impar
                    imprimirEntrada(segundoJugador);
                    

                    if (segundoJugador == "maquina")
                    {
                        //***********************************************************************************************************************************************************
                        //propuesta de la máquina
                        String prioridad = "MAQUINA";
                        int opc = generarPropuestaMaquina(); //selecciona cuál es el criterio de victoria de la ronda
                        imprimirElección("MAQUINA", opc);
                        int movi = seleccionarCartaMaquina(maquina);
                        maquina = adicionales(maquina, movi, "MAQUINA", opc); //función para aplicar adicionales
                        imprimirEleccionCarta("MAQUINA", maquina[movi]);

                        //guardar el objeto antes de eliminarlo
                        carta SelMa = maquina[movi];

                        //eliminar la carta del mazo 
                        maquina[movi] = null;
                        Console.ReadKey();
                        //***********************************************************************************************************************************************************


                        //***********************************************************************************************************************************************************
                        //respuesta del usuario
                        Console.WriteLine("\nTURNO DEL USUARIO");
                        bool selec = false;
                        movi = seleccionarCartaUsuario(usuario);
                        usuario = adicionales(usuario, movi, "USUARIO", opc);
                        imprimirEleccionCarta("USUARIO", usuario[movi]);

                        //guardar el objeto antes de eliminarlo
                        carta SelUs = usuario[movi];

                        //eliminar la carta del mazo 
                        usuario[movi] = null;
                        //***********************************************************************************************************************************************************


                        //***********************************************************************************************************************************************************
                        //COMPARACIÓN DE QUIÉN GANA
                        //genero un arreglo con lo recibido por el metodo que decide el ganador del duelo
                        int[] respuesta = ganadorDuelo(opc, SelMa, SelUs, puntosMaquina, puntosUsuario, prioridad);
                        puntosUsuario = respuesta[0];
                        puntosMaquina = respuesta[1];
                        //***********************************************************************************************************************************************************
                    }
                    else
                    {
                        //***********************************************************************************************************************************************************
                        //Propuesta del usuario
                        String prioridad = "USUARIO";
                        int opc = mostrarEleccionUsuario();
                        imprimirElección("USUARIO", opc);

                        bool selec = false;
                        int movi = 0;
                        movi = seleccionarCartaUsuario(usuario);
                        usuario = adicionales(usuario, movi, "USUARIO", opc);
                        imprimirEleccionCarta("USUARIO", usuario[movi]);

                        //guardamos la carta antes de eliminarla
                        carta SelUs = usuario[movi];

                        //eliminar la carta del mazo 
                         usuario[movi] = null;
                        //***********************************************************************************************************************************************************


                        //***********************************************************************************************************************************************************
                        //respuesta de la máquina
                        Console.WriteLine("\nTURNO DE LA MÁQUINA");
                        
                        Console.ReadKey();
                        selec = false;
                        movi = seleccionarCartaMaquina(maquina);
                        maquina = adicionales(maquina, movi, "MAQUINA", opc); //función para aplicar adicionales
                        imprimirEleccionCarta("MAQUINA", maquina[movi]);

                        //guardo en un objeto llamado seleccion de la maquina, el objeto antes de borrarlo para compararlo
                        carta SelMa = maquina[movi];

                        //eliminar la carta del mazo 
                        maquina[movi] = null;
                        //***********************************************************************************************************************************************************


                        //***********************************************************************************************************************************************************
                        //COMPARACIÓN DE QUIÉN GANA
                        //genero un arreglo con lo recibido por el metodo que decide el ganador del duelo
                        int[] respuesta = ganadorDuelo(opc, SelMa, SelUs, puntosMaquina, puntosUsuario, prioridad);
                        puntosUsuario = respuesta[0];
                        puntosMaquina = respuesta[1];
                        //***********************************************************************************************************************************************************
                    }
                }

                contador = contador + 1;
            }

            //definir el ganador de la partida
            Console.Clear();
            Console.WriteLine("Resultados de la partida: ");
            Console.WriteLine("\n\nPuntuación: ");
            Console.WriteLine("Usuario: ");
            Console.WriteLine(puntosUsuario);
            Console.WriteLine("Máquina: ");
            Console.WriteLine(puntosMaquina);

            if (puntosMaquina > puntosUsuario)
                Console.WriteLine("\n\nLa máquina ganó la partida");
            else
                Console.WriteLine("\n\nEl usuario ganó la partida");
        }

        //función para cargar la biblioteca de cartas y los decks
        public carta[] cargarCartas()
        {
            //se comienza con la creación del stock de cartas con que se trabajará - total de 30 cartas
            carta a1 = new carta(1, "Bruja blanca", "guerrero", 800, 500, 1000);
            carta a2 = new carta(2, "Bruja azul", "magia", 700, 400, 3000);
            carta a3 = new carta(3, "Bruja roja", "equipo", 200, 500, 5000);
            carta a4 = new carta(4, "Bruja celeste", "diplomacia", 600, 400, 1000);
            carta a5 = new carta(5, "Bruja morada", "guerrero", 900, 200, 5000);
            carta a6 = new carta(6, "Bruja negra", "magia", 50, 5000, 1000);
            carta a7 = new carta(7, "Bruja rosada", "equipo", 150, 4800, 1000);
            carta a8 = new carta(8, "Bruja gris", "diplomacia", 300, 500, 1000);
            carta a9 = new carta(9, "Bruja escarlata", "guerrero", 850, 510, 1000);
            carta a0 = new carta(10, "Bruja verde", "magia", 450, 580, 1000);
            carta b1 = new carta(11, "Duende blanco", "equipo", 125, 8000, 1000);
            carta b2 = new carta(12, "Duende azul", "diplomacia", 200, 5000, 1000);
            carta b3 = new carta(13, "Duende rojo", "guerrero", 1250, 50, 1000);
            carta b4 = new carta(14, "Duende celeste", "magia", 80, 50, 1000);
            carta b5 = new carta(15, "Duende morado", "equipo", 8, 5, 1000);
            carta b6 = new carta(16, "Duende negro", "diplomacia", 8000, 5000, 1000);
            carta b7 = new carta(17, "Duende rosado", "guerrero", 777, 555, 1000);
            carta b8 = new carta(18, "Duende gris", "magia", 888, 111, 1000);
            carta b9 = new carta(19, "Duende escarlata", "equipo", 800, 147, 1000);
            carta b0 = new carta(20, "Duende verde", "diplomacia", 478, 525, 1000);
            carta c1 = new carta(21, "Dragon blanco", "guerrero", 278, 5000, 1000);
            carta c2 = new carta(22, "Dragon azul", "magia", 8000, 50, 1000);
            carta c3 = new carta(23, "Dragon rojo", "equipo", 222, 502, 1000);
            carta c4 = new carta(24, "Dragon celeste", "diplomacia", 1800, 1500, 1000);
            carta c5 = new carta(25, "Dragon morado", "guerrero", 2800, 2500, 1000);
            carta c6 = new carta(26, "Dragon negro", "magia", 3800, 3500, 1000);
            carta c7 = new carta(27, "Dragon rosado", "equipo", 4800, 4500, 1000);
            carta c8 = new carta(28, "Dragon gris", "diplomacia", 5800, 5500, 1000);
            carta c9 = new carta(29, "Dragon escarlata", "guerrero", 6800, 6500, 1000);
            carta c0 = new carta(30, "Dragon verde", "magia", 7800, 7500, 1000);

            //crear el array con las cartas y agregarlas
            carta[] stock = new carta[30];
            stock[0] = a0;
            stock[1] = a1;
            stock[2] = a2;
            stock[3] = a3;
            stock[4] = a4;
            stock[5] = a5;
            stock[6] = a6;
            stock[7] = a7;
            stock[8] = a8;
            stock[9] = a9;
            stock[10] = b0;
            stock[11] = b1;
            stock[12] = b2;
            stock[13] = b3;
            stock[14] = b4;
            stock[15] = b5;
            stock[16] = b6;
            stock[17] = b7;
            stock[18] = b8;
            stock[19] = b9;
            stock[20] = c0;
            stock[21] = c1;
            stock[22] = c2;
            stock[23] = c3;
            stock[24] = c4;
            stock[25] = c5;
            stock[26] = c6;
            stock[27] = c7;
            stock[28] = c8;
            stock[29] = c9;

            return stock;
        }

        //función para cargar el deck del usuario
        public carta[] deckUsuario(carta[] stock)
        {
            carta[] usuario = new carta[10];
            usuario[0] = stock[0];
            usuario[1] = stock[1];
            usuario[2] = stock[5];
            usuario[3] = stock[9];
            usuario[4] = stock[13];
            usuario[5] = stock[17];
            usuario[6] = stock[18];
            usuario[7] = stock[20];
            usuario[8] = stock[22];
            usuario[9] = stock[23];
            return usuario;
        }

        //función para cargar el deck de la máquina
        public carta[] deckMaquina(carta[] stock)
        {
            carta[] maquina = new carta[10];
            bool aux = false;

            while(aux==false)
            {
                //randomicamente generamos el mazo de la maquina
                var rnd = new Random();
                var randomNumbers = Enumerable.Range(0, 29).OrderBy(x => rnd.Next()).Take(10).ToList();

                //crear el mazo de la máquina
                int i;
                for (i = 0; i < 10; i++)
                {
                    maquina[i] = stock[randomNumbers[i]];
                }

                int contador = 0;
                //comprobar al menos 5 guerreros
                foreach(carta card in maquina)
                {
                    if (card.getTipo() == "guerrero") contador = contador + 1;
                }

                if (contador == 5) aux = true;
            }
            return maquina;
        }

        //función para el cara y sello usuario vs máquina
        public String[] caraSello(String deseoJugador)
        {
            //algoritmo de la moneda para saber quién comienza - 0 = cara - 1 = sello
            int apuJugador;
            String primerJugador = " ", segundoJugador = " ";


            if (deseoJugador == "cara")
            {
                Console.WriteLine("El usuario escoge cara y la maquina sello");
                apuJugador = 0;
            }
            if (deseoJugador == "sello")
            {
                Console.WriteLine("El usuario escoge sello y la maquina cara");
                apuJugador = 1;
            }

            var rnd1 = new Random();
            var randomNumber = rnd1.Next(2);

            int comienzo = randomNumber;

            if (comienzo == 0)
            {
                Console.WriteLine("Cayó cara");
                if (deseoJugador == "cara")
                {
                    Console.WriteLine("Por lo tanto, el primer turno es del usuario");
                    primerJugador = "usuario";
                    segundoJugador = "maquina";
                }
                if (deseoJugador == "sello")
                {
                    Console.WriteLine("Por lo tanto, el primer turno es de la máquina");
                    primerJugador = "maquina";
                    segundoJugador = "usuario";
                }

            }
            else
            {
                Console.WriteLine("Cayó sello");
                if (deseoJugador == "cara")
                {
                    Console.WriteLine("\n\nPor lo tanto, el primer turno es de la maquina");
                    primerJugador = "maquina";
                    segundoJugador = "usuario";
                }
                if (deseoJugador == "sello")
                {
                    Console.WriteLine("Por lo tanto, el primer turno es del usuario");
                    primerJugador = "usuario";
                    segundoJugador = "maquina";

                }
            }

            //creación del array a devolver
            String[] posiciones = new string[2];
            posiciones[0] = primerJugador;
            posiciones[1] = segundoJugador;

            return posiciones;

        }
            
        //funcion que imprime de quién es la entrada
        public void imprimirEntrada(String player)
        {
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("\n\nEntrada para: ");
            Console.WriteLine(player);
        }

        //función que imprime el número de ronda
        public void imprimirRonda(int contador)
        {
            Console.Clear();
            Console.WriteLine("\n\nRONDA N. ");
            Console.WriteLine(contador+1);
        }

        //función que imprime la elección de juego
        public void imprimirElección(String player, int opc)
        {
            switch (opc)
            {
                case 1:
                    Console.WriteLine(player + " ha elegido que Gana la carta con mayor ataque: ");
                    break;

                case 2:
                    Console.WriteLine(player + " ha elegido que Gana la carta con menor ataque: ");
                    break;

                case 3:
                    Console.WriteLine(player + " ha elegido que Gana la carta con mayor defensa: ");
                    break;

                case 4:
                    Console.WriteLine(player+" ha elegido que Gana la carta con menor defensa: ");
                    break;

            }
        }

        //función que asigna e imprime el ganador de un duelo
        public int[] ganadorDuelo(int opc, carta SelMa, carta SelUs, int puntosMaquina, int puntosUsuario, String prioridad)
        {
            //COMPARACIÓN DE QUIÉN GANA

            Console.WriteLine("Por lo tanto, esta ronda la gana: ");

            switch (opc)
            {
                case 1:
                    if (SelMa.getAtaque() > SelUs.getAtaque())
                    {
                        Console.WriteLine("LA MAQUINA");
                        puntosMaquina = puntosMaquina + 1;
                    }
                    else if(SelMa.getAtaque() < SelUs.getAtaque())
                    {
                        Console.WriteLine("EL USUARIO");
                        puntosUsuario = puntosUsuario + 1;
                    }
                    else if(prioridad == "MAQUINA")
                    {
                        puntosMaquina = puntosMaquina + 1;
                    }
                    else if(prioridad == "USUARIO")
                    {
                        puntosUsuario = puntosUsuario + 1;
                    }
                        

                    break;

                case 2:
                    if (SelMa.getAtaque() < SelUs.getAtaque())
                    {
                        Console.WriteLine("LA MAQUINA");
                        puntosMaquina = puntosMaquina + 1;
                    }
                    else if (SelMa.getAtaque() > SelUs.getAtaque())
                    {
                        Console.WriteLine("EL USUARIO");
                        puntosUsuario = puntosUsuario + 1;
                    }
                    else if (prioridad == "MAQUINA")
                    {
                        puntosMaquina = puntosMaquina + 1;
                    }
                    else if (prioridad == "USUARIO")
                    {
                        puntosUsuario = puntosUsuario + 1;
                    }

                    break;

                case 3:
                    if (SelMa.getDefensa() > SelUs.getDefensa())
                    {
                        Console.WriteLine("LA MAQUINA");
                        puntosMaquina = puntosMaquina + 1;
                    }
                    else if (SelMa.getDefensa() < SelUs.getDefensa())
                    {
                        Console.WriteLine("EL USUARIO");
                        puntosUsuario = puntosUsuario + 1;
                    }
                    else if (prioridad == "MAQUINA")
                    {
                        puntosMaquina = puntosMaquina + 1;
                    }
                    else if (prioridad == "USUARIO")
                    {
                        puntosUsuario = puntosUsuario + 1;
                    }


                    break;

                case 4:
                    if (SelMa.getDefensa() < SelUs.getDefensa())
                    {
                        Console.WriteLine("LA MAQUINA");
                        puntosMaquina = puntosMaquina + 1;
                    }
                    else if (SelMa.getDefensa() < SelUs.getDefensa())
                    {
                        Console.WriteLine("EL USUARIO");
                        puntosUsuario = puntosUsuario + 1;
                    }
                    else if (prioridad == "MAQUINA")
                    {
                        puntosMaquina = puntosMaquina + 1;
                    }
                    else if (prioridad == "USUARIO")
                    {
                        puntosUsuario = puntosUsuario + 1;
                    }

                    break;

            }

            int[] resultado = new int[2];
            resultado[0] = puntosUsuario;
            resultado[1] = puntosMaquina;

            Console.ReadKey();
            return resultado;
        }

        //función que imprime la carta seleccionada
        public void imprimirEleccionCarta(String player, carta obj)
        {
            Console.WriteLine("\n\n" + player + " ha elegido la carta: ");
            Console.WriteLine("Nombre de carta: ");
            Console.WriteLine(obj.getNombre());
            Console.WriteLine("Tipo: ");
            Console.WriteLine(obj.getTipo());
            Console.WriteLine("Ataque: ");
            Console.WriteLine(obj.getAtaque());
            Console.WriteLine("Defensa: ");
            Console.WriteLine(obj.getDefensa());
            Console.WriteLine("Poder de minado: ");
            Console.WriteLine(obj.getPoderMinado());
        }

        //función que imprime la elección de juego del usuario
        public int mostrarEleccionUsuario()
        {
            Console.WriteLine("TURNO DEL USUARIO");
            //propuesta del usuario
            Console.WriteLine("\nOpciones de duelo: ");
            Console.WriteLine("1. Gana la carta con mayor ataque");
            Console.WriteLine("2. Gana la carta con menor ataque");
            Console.WriteLine("3. Gana la carta con mayor defensa");
            Console.WriteLine("4. Gana la carta con menor defensa");
            int opc = Convert.ToInt32(Console.ReadLine());

            return opc;
        }

        //función que genera la propuesta de juego de la máquina 
        public int generarPropuestaMaquina()
        {
            int opc=0;
            Console.WriteLine("TURNO DE LA MÁQUINA");
            var rnd2 = new Random();
            var randomNumber3 = Enumerable.Range(1, 4).OrderBy(x => rnd2.Next()).Take(1).ToList();
            opc = randomNumber3[0];
            Console.ReadKey();
            return opc;
        }

        //función que selecciona la carta de la máquina
        public int seleccionarCartaMaquina(carta[] maquina)
        {
            int movi = 0;

            bool selec = false;
            while (selec == false)
            {
                //seleccion de carta de propuesta
                var rnd4 = new Random();
                var randomNumber4 = Enumerable.Range(0, maquina.Length).OrderBy(x => rnd4.Next()).Take(1).ToList();
                int mov = randomNumber4[0];

                if (maquina[mov] != null && maquina[mov].getTipo()=="guerrero")
                {
                    selec = true;
                    movi = mov;
                }
            }

            return movi;
        }

        public int seleccionarCartaUsuario(carta[] usuario)
        {
            int movi = 0; ;
            bool selec;
            selec = false;
            while (selec == false)
            {
                int cont = 0;
                //imprimir el mazo del usuario
                Console.WriteLine("\n\nEscoja una carta  : ");
                imprimirMazo(usuario, "USUARIO");

                //seleccion de carta de respuesta
                int mov = Convert.ToInt32(Console.ReadLine());
                if (usuario[mov] != null && usuario[mov].getTipo() == "guerrero")
                {
                    selec = true;
                    movi = mov;
                }
                else
                    Console.WriteLine("Escoja un numero de carta valido, asegúrese que sea un guerrero");
            }
            return movi;
        }

        //función que imprime el mazo de cartas
        public void imprimirMazo(carta[] mazo, String player)
        {
            int contador = 0;
            Console.WriteLine("\n\nMazo de: "+player);
            Console.WriteLine("\n\nNombre - tipo - ataque - defensa - poder de minado");
            foreach (carta aux in mazo)
            {
                
                if (aux != null)
                {
                    String nombre = aux.getNombre();
                    String tipo = aux.getTipo();
                    int ataque = aux.getAtaque();
                    int defensa = aux.getDefensa();
                    int poderMinado = aux.getPoderMinado();

                    Console.WriteLine(contador+" "+nombre+" "+tipo+" "+ataque+" "+defensa+" "+poderMinado);
                }
                contador = contador + 1;
            }

            
        }

        //función que aplica efectos adicionales al guerrero
        public carta[] adicionales(carta[] mazo, int puntero, String player, int opc)
        {

            //se imprime la carta de guerrero inicial
            Console.WriteLine("\n\n+"+player+" ha usado un guerrero ");
            imprimirEleccionCarta(player, mazo[puntero]);

            if(mazo.Length>0)
            {
                if (player == "MAQUINA") //si el jugador es la maquina, aplicación de la carta adicional
                {

                    bool mien = false;

                    while (mien == false)
                    {
                        Random x = new Random();
                        int resp = x.Next(mazo.Length);
                        if (mazo[resp].getTipo() == "magia" || mazo[resp].getTipo() == "equipo") // si el randomico generado es magica o de equipo para aumentar mis stats
                        {
                            //se toma y se aplica esa carta
                            Console.WriteLine("\n\nLa máquina ha usado una carta adicional de " + mazo[resp].getTipo());
                            imprimirEleccionCarta("MAQUINA", mazo[resp]);
                            //aplico a la seleccionada anteriormente -- la aplico de acuerdo a la opción
                            switch (opc)
                            {
                                case 1:
                                    mazo[puntero].setAtaque(mazo[puntero].getAtaque() + mazo[resp].getAtaque()); //aplico el aumento de stats
                                    break;
                                case 2:
                                    mazo[puntero].setAtaque(mazo[puntero].getAtaque() - mazo[resp].getAtaque()); //aplico la disminución de stats
                                    break;

                                case 3:
                                    mazo[puntero].setDefensa(mazo[puntero].getDefensa() + mazo[resp].getDefensa()); //aplico el aumento de stats
                                    break;

                                case 4:
                                    mazo[puntero].setDefensa(mazo[puntero].getDefensa() - mazo[resp].getDefensa()); //aplico la disminución de stats
                                    break;
                            }
                            //borro la aplicada
                            mazo[resp] = null;
                            mien = true;
                        }
                    }


                }
                else if (player == "USUARIO") //aplicación de cartas adicionales para el usuario //si el jugador es el usuario, se le listan las cartas y se aplica de la carta adicional
                {

                    Console.WriteLine("\n\nDesea el usuario agregar otra carta de estrategia? (si/no)");
                    String respu = Console.ReadLine();

                    while (respu == "si")
                    {

                        //RESP LA OTRA CARTA ADICIONAL
                        int resp;
                        bool selec;
                        selec = false;


                        int cont = 0;
                        //imprimir el mazo del usuario
                        Console.WriteLine("\n\nEscoja una carta  : ");
                        imprimirMazo(mazo, "USUARIO");

                        //seleccion de carta de respuesta
                        resp = Convert.ToInt32(Console.ReadLine());







                        if (mazo[resp].getTipo() == "magia" || mazo[resp].getTipo() == "equipo") // si el randomico generado es magica o de equipo para aumentar mis stats
                        {
                            //se toma y se aplica esa carta
                            Console.WriteLine("\n\nEl usuario ha usado una carta adicional de " + mazo[resp].getTipo());
                            imprimirEleccionCarta("USUARIO", mazo[resp]);
                            //aplico a la seleccionada anteriormente -- la aplico de acuerdo a la opción
                            switch (opc)
                            {
                                case 1:
                                    mazo[puntero].setAtaque(mazo[puntero].getAtaque() + mazo[resp].getAtaque()); //aplico el aumento de stats
                                    break;
                                case 2:
                                    mazo[puntero].setAtaque(mazo[puntero].getAtaque() - mazo[resp].getAtaque()); //aplico la disminución de stats
                                    break;

                                case 3:
                                    mazo[puntero].setDefensa(mazo[puntero].getDefensa() + mazo[resp].getDefensa()); //aplico el aumento de stats
                                    break;

                                case 4:
                                    mazo[puntero].setDefensa(mazo[puntero].getDefensa() - mazo[resp].getDefensa()); //aplico la disminución de stats
                                    break;
                            }
                            //borro la aplicada
                            mazo[resp] = null;

                        }
                        else
                        {
                            Console.WriteLine("Escoja una carta válida");
                            Console.ReadLine();
                        }


                        Console.WriteLine("\n\nDesea el agregar otra carta de estrategia? (si/no)");
                        respu = Console.ReadLine();
                    }

                }
            }
            else
            {
                Console.WriteLine("\n\nNo hay cartas de estrategia para aplicar");
                Console.ReadLine();
            }

            

            return mazo;
        }


    }
}