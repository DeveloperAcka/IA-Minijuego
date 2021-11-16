using System;
using System.Collections.Generic;
using System.Text;

namespace IA
{
    class carta
    {

        //declaracion de parametros de la carta
        private String nombre;
        private int ataque;
        private int defensa;
        private int poderMinado;

        //constructor
        public carta(String nombre, int ataque, int defensa, int poderMinado)
        {
            this.nombre = nombre;
            this.ataque = ataque;
            this.defensa = defensa;
            this.poderMinado = poderMinado;

        }

        //get del campo nombre
        public String getNombre()
        {
            return this.nombre;
        }

        //get del campo ataque
        public int getAtaque()
        {
            return this.ataque;
        }

        //get del campo defensa
        public int getDefensa()
        {
            return this.defensa;
        }

        //get del campo poder de minado
        public int getPoderMinado()
        {
            return this.poderMinado;
        }
    }
}
