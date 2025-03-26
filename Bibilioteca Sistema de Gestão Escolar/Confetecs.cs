using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibilioteca_Sistema_de_Gestão_Escolar
{
    public class Confete
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float SpeedX { get; set; }
        public float SpeedY { get; set; }
        public Color Cor { get; set; }

        private static Random random = new Random();

        public Confete(float x, float y)
        {
            X = x;
            Y = y;
            SpeedX = (float)(random.NextDouble() * 4 - 2); // Velocidade horizontal aleatória
            SpeedY = (float)(random.NextDouble() * 5 + 1); // Velocidade vertical aleatória
            Cor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)); // Cor aleatória
        }

        public void Atualizar()
        {
            X += SpeedX;
            Y += SpeedY;
            SpeedY += 0.1f; // Simulação de gravidade
        }

        // Desenha o confete usando o Graphics fornecido
        public void Desenhar(Graphics g)
        {
            using (var brush = new SolidBrush(Cor)) // O uso do SolidBrush deve ser dentro de um bloco using para descarte
            {
                g.FillEllipse(brush, X, Y, 5, 5); // Desenha um círculo representando o confete
            }
        }

    }
}
