using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication43
{

    class Program
    {
        enum Suit
        {
            Spade, Heart, Diamond, Club
        }
        enum Value
        {
            Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queue, King, Ace, Two
        }
        class Card
        {
            public Suit suit;
            public Value value;
        }
        static void Main(string[] args)
        {
            Card[] c = new Card[52];
            for (int i = 0; i < 52; i++)   ///初始化一副扑克牌
            {
                c[i] = new Card();
                c[i].suit = (Suit)(i % 4);
                c[i].value = (Value)(i % 13);
            }
            for (int k = 51; k >= 0; k--)  //洗牌
            {
                Random rand = new Random();
                int p = rand.Next(k);
                Card temp = new Card();
                temp = c[p];
                c[p] = c[k];
                c[k] = temp;
            }
            Card[] E = new Card[13]; Card[] W = new Card[13];
            Card[] S = new Card[13]; Card[] N = new Card[13];
            int A = 0, B = 0, C = 0, D = 0;
            for (int j = 0; j < 52; j++)//分牌
            {
                switch (j % 4)
                {
                    case 0: { E[A++] = c[j]; break; }
                    case 1: { S[B++] = c[j]; break; }
                    case 2: { W[C++] = c[j]; break; }
                    case 3: { N[D++] = c[j]; break; }
                    default: { Console.WriteLine("error"); break; }
                }
            }
            Console.WriteLine("东：");
            for (int i = 0; i < E.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (E[i+1].value < E[i].value)
                    {
                        var temp2 = E[i].value;
                        E[i].value = E[i+1].value;
                        E[i+1].value = temp2;
                      
                        if (E[j+1].suit < E[j].suit)
                        {
                            var temp1 = E[j].suit;
                            E[j].suit = E[j+1].suit;
                            E[j+1].suit = temp1;

                        }
                    }
  
                }
                 Console.Write("{0},{1} ", S[i].suit, S[i].value);
            }
            Console.WriteLine();
            Console.WriteLine("南：");
            for (int i = 0; i < 13; i++)
            { Console.Write("{0},{1} ", S[i].suit, S[i].value); }
            Console.WriteLine();
            Console.WriteLine("西：");
            for (int i = 0; i < 13; i++)
            { Console.Write("{0},{1} ", W[i].suit, W[i].value); }
            Console.WriteLine();
            Console.WriteLine("北：");
            for (int i = 0; i < 13; i++)
            { Console.Write("{0},{1} ", N[i].suit, N[i].value); }
        }
    }
}