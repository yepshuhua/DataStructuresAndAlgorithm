using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 洗扑克牌
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] cardColor = { "方块", "梅花", "红桃", "黑桃" };
            int[] cardXia = {
                    44,48,0,4,8,12,16,20,24,28,32,36,40,
                    45,49,1,5,9,13,17,21,25,29,33,37,41,
                    46,50,2,6,10,14,18,22,26,30,34,38,42,
                    47,51,3,7,11,15,19,23,27,31,35,39,43
            };//以方块三最小原则排序
            string[] cardWang = { "大王", "小王" };
            int[] wangXia = { 52, 53 };
            var cardList = new List<Card>();
            int colorIndex = 0;
            int tempValue = 0;
            for (int i = 0; i < cardXia.Length; i++)//有点数牌
            {
                if (i % 13 == 0 && i != 0)//判断花色牌数，达到13取下一个花色
                {
                    colorIndex++;
                }

                Char a;
                tempValue = i % 13;
                switch (tempValue)
                {
                    case 0:
                        a = 'A';
                        break;
                    case 9:
                        a = '十';
                        break;
                    case 10:
                        a = 'J';
                        break;
                    case 11:
                        a = 'Q';
                        break;
                    case 12:
                        a = 'K';
                        break;
                    default:
                        a = (Char)(tempValue + (int)'1');
                        break;
                }
                Card vm = new Card//初始化赋值卡牌属性
                {
                    value = a,
                    color = cardColor[colorIndex],
                    xia = cardXia[i]
                };
                cardList.Add(vm);
            }
            for (int i = 0; i < cardWang.Length; i++)  //无点数牌====王
            {
                Card vm = new Card
                {
                    color = cardWang[i],
                    xia = wangXia[i]
                };
                cardList.Add(vm);
            }
            List<Card> ShuffleCardEnd = ShuffleCard(cardList);
            /////斗地主四人打牌51张分配，一次性发完个人的牌，留最后3张底牌
            List<Card> one = new List<Card>();
            List<Card> tow = new List<Card>();
            List<Card> three = new List<Card>();
            List<Card> four = new List<Card>();
            List<Card> glide = new List<Card>();
            for (int i = 0; i < ShuffleCardEnd.Count; i++)
            {

                if (i < 13)
                {
                    Card vm = new Card()
                    {
                        color = ShuffleCardEnd[i].color,
                        value = ShuffleCardEnd[i].value,
                        xia = ShuffleCardEnd[i].xia
                    };
                    one.Add(vm);
                }
                else if (i < 26)
                {
                    Card vm = new Card()
                    {
                        color = ShuffleCardEnd[i].color,
                        value = ShuffleCardEnd[i].value,
                        xia = ShuffleCardEnd[i].xia
                    };
                    tow.Add(vm);
                }
                else if (i < 39)
                {
                    Card vm = new Card()
                    {
                        color = ShuffleCardEnd[i].color,
                        value = ShuffleCardEnd[i].value,
                        xia = ShuffleCardEnd[i].xia
                    };
                    three.Add(vm);
                }

                else if (i < 51)
                {
                    Card vm = new Card()
                    {
                        color = ShuffleCardEnd[i].color,
                        value = ShuffleCardEnd[i].value,
                        xia = ShuffleCardEnd[i].xia
                    };
                    four.Add(vm);
                }
                else
                {
                    Card vm = new Card()
                    {
                        color = ShuffleCardEnd[i].color,
                        value = ShuffleCardEnd[i].value,
                        xia = ShuffleCardEnd[i].xia
                    };
                    glide.Add(vm);
                }

            }
            //输出
            Console.WriteLine("发牌每人得到：");
            Console.Write("东手上的牌是：");
            ShowCard(one);
            Console.Write("西手上的牌是：");
            ShowCard(tow);
            Console.Write("南手上的牌是：");
            ShowCard(three);
            Console.Write("北手上的牌是：");
            ShowCard(four);
            Console.WriteLine();
            Console.WriteLine("===========================");
            Console.Write("底牌是：");
            ShowCard(glide);
            Console.WriteLine();
            Console.WriteLine("各自手上排序后：");
            Console.Write("东手上的牌是：");
            ShowCards(one);
            Console.Write("西手上的牌是：");
            ShowCards(tow);
            Console.Write("南手上的牌是：");
            ShowCards(three);
            Console.Write("北手上的牌是：");
            ShowCards(four);
            Console.ReadLine();
        }
        /// <summary>
        /// 随机洗牌
        /// </summary>
        /// <param name="cardList"></param>
        /// <returns></returns>
        public static List<Card> ShuffleCard(List<Card> cardList)
        {
            Random random = new Random();
            int tempIndex = 0;
            Card temp = null;
            for (int i = 0; i < 54; i++)
            {
                tempIndex = random.Next(54);
                temp = cardList[tempIndex];
                cardList[tempIndex] = cardList[i];
                cardList[i] = temp;
            }

            return cardList;
        }
        /// <summary>
        /// 显示数组元素
        /// </summary>
        /// <param name="cardList"></param>
        static void ShowCard(List<Card> cardList)
        {
            for (int i = 0; i < cardList.Count; i++)
            {


                Console.Write(cardList[i].color + "" + cardList[i].value + " ");
                if (i % 12 == 0 && i != 0)
                {
                    Console.WriteLine("\n");
                }
            }

        }
        /// <summary>
        /// 冒泡排序后显示
        /// </summary>
        /// <param name="cardList"></param>
        static void ShowCards(List<Card> cardList)
        {
            Card aa = null;

            for (int j = 0; j < cardList.Count; j++)
            {
                for (int i = 0; i < cardList.Count - 1; i++)
                {

                    if (cardList[i].xia > cardList[i + 1].xia)
                    {
                        aa = cardList[i];
                        cardList[i] = cardList[i + 1];
                        cardList[i + 1] = aa;

                    }

                }
            }
            for (int i = 0; i < cardList.Count; i++)//显示排序好的卡牌
            {

                Console.Write(cardList[i].color + "" + cardList[i].value + " ");

            }
            Console.WriteLine("\n");
        }


    }

    public class Card
    {
        public char value;//卡牌点数
        public string color;//卡牌花色
        public int xia;//卡牌下标
    }


}