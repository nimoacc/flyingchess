// See https://aka.ms/new-console-template for more information

using FlyingChess;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Threading;
using System;

Console.WriteLine("Hello, World!\n来玩飞行棋吧。");
Console.WriteLine("请输入游戏人数！1-4。");
int players = 0;
while (true)
{
    try { players = Convert.ToInt32(Console.ReadLine()); if(players!=1&&players!=2&&players!=3&&players!=4) Console.WriteLine("输入错误！请重新输入。"); else break; }
    catch { Console.WriteLine("输入错误！请重新输入。"); }
}
Console.WriteLine("请输入动画速度！通常可以选择1倍，2倍，5倍或者10倍速。");
int speed = 1;
while (true) { 
try{speed = Convert.ToInt32(Console.ReadLine());break; }
catch { Console.WriteLine("输入错误！请重新输入。"); }
}

int[,] pieces = new int[4,4];

string[] names = new string[4];
string c = "BRYGBRYGGGGYGBYRGRRYGBGYGBRBGYGGGYYYYYYYBBBBBBBRRRBRYGYRBRYRBGGRGBYRBRRRRBGYRBGY";
ConsoleColor[] t = { ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Red, };
string[] ini = {"4\t\t\t\tB\tR\tY\tG\tB\tR\tY\tG\t\t\t4\r\n" ,
    "\t\t\t\tG\t\t\tG\t\t\tG\t\t\t\t\r\n" ,
    "\t\t\t\tY\t\t\tG\t\t\tB\t\t\t\t\r\n" ,
    "Y\t\t\t\tR\t→\t→\tG\t→\t→\tR\t\t\t\t\r\n" ,
    "R\tY\tG\tB\t/\t\t\tG\t\t\t\\\tY\tG\tB\tR\r\n" ,
    "B\t\t\t↑\t\t\t\tG\t\t\t\t↓\t\t\tY\r\n" ,
    "G\t\t\t↑\t\t\t\tG\t\t\t\t↓\t\t\tG\r\n" ,
    "Y\tY\tY\tY\tY\tY\tY\t\tB\tB\tB\tB\tB\tB\tB\r\n" ,
    "R\t\t\t↑\t\t\t\tR\t\t\t\t↓\t\t\tR\r\n" ,
    "B\t\t\t↑\t\t\t\tR\t\t\t\t↓\t\t\tY\r\n" ,
    "G\tY\tR\tB\t\\\t\t\tR\t\t\t/\tY\tR\tB\tG\r\n" ,
    "\t\t\t\tG\t←\t←\tR\t←\t←\tG\t\t\t\tB\r\n" ,
    "\t\t\t\tY\t\t\tR\t\t\tB\t\t\t\t\r\n" ,
    "\t\t\t\tR\t\t\tR\t\t\tR\t\t\t\t\r\n" ,
    "4\t\t\tR\tB\tG\tY\tR\tB\tG\tY\t\t\t\t4\r\n"};
List<Points> b = new List<Points>();
int[,] ps = { { 0, 4 }, { 0, 5 }, { 0, 6 },{ 0, 7 },{ 0, 8 },{ 0, 9 },{ 0, 10 },{ 0, 11 },{ 1, 4 },{ 1, 7 },{ 1, 10 }, { 2, 4 },{ 2, 7 },{ 2, 10 },
    { 3, 1 },{ 3, 4 },{ 3, 7 },{ 3, 10 }, { 4, 0 },{ 4, 1 },{ 4, 2 },{ 4, 3 }, { 4, 7 },{ 4, 11 },{ 4, 12 },{ 4, 13 },{ 4, 14 },{ 5, 0 },{ 5, 7 },{ 5, 14 }, { 6, 0 },{ 6, 7 },{ 6, 14 },
    { 7, 0 },{ 7, 1 },{ 7, 2 },{ 7, 3 },{ 7, 4 },{ 7, 5 },{ 7, 6 },{ 7, 8 },{ 7, 9 },{ 7, 10 },{ 7, 11 },{ 7, 12 },{ 7, 13 },{ 7, 14 } ,
    { 8, 0}, { 8, 7 },{ 8, 14 },{9,0 },{9,7 },{9,14 }, {10,0 },{10,1 },{10,2 },{10,3 },{10,7 },{10,11 },{10,12 },{10,13 },{10,14 },
    {11,4 },{11,7 },{11,10 },{11,14 },{12,4 },{12,7 },{12,10 },{13,4 },{13,7 },{13,10 },{14,3 },{14,4 },{14,5 },{14,6 },{14,7 },{14,8 },{14,9 } ,{14,10 } };

int[] next = { 1, 2, 3, 4,5,6,10,6,0,12,13,8,16,17,18,11,22,23,19,20,21,15,28,24,25,26,29,18,31,32,27,-1,46,30,35,36,37,38,39,-1,-1,40,41,42,43,44,49,33,-1,52,47,48,61,50,53,54,55,51,64,58,59,60,56,57,68,61,62,63,71,66,67,79,73,69,73,74,75,76,77,78};
for (int i = 0; i < 80; i++)
{
    Points A = new Points(i, ConsoleColor.Blue, new List<Points>());
    switch (c[i]) {
        case 'R':
            A.color = ConsoleColor.Red;
            break;
        case 'Y':
            A.color = ConsoleColor.Yellow;
            break;
        case 'B':
            A.color = ConsoleColor.Blue;
            break;
        case 'G':
            A.color = ConsoleColor.Green;
            break;
    }
    b.Add(A);
}

for (int i = 0; i < 80; i++)
    if (next[i]!=-1)
        b[i].next.Add(b[next[i]]);   
b[3].next.Add(b[9]);
b[33].next.Add(b[34]);
b[46].next.Add(b[45]);
b[76].next.Add(b[70]);
b[23].next.Add(b[43]);
b[64].next.Add(b[63]);
b[56].next.Add(b[36]);
b[15].next.Add(b[16]);
/*
for (int i = 0; i < 80; i++)
{
    foreach(Points j in b[i].next)
    {
        Console.Write(j.x);
        Console.Write('\t');
    }
    Console.WriteLine();
}
*/
    for (int i = 0; i < players; i++)
{ 
    
    Console.WriteLine("输入玩家{0}姓名！",i+1);
    while (true)
    {
        string? a = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(a))
        {
            Console.WriteLine("输入不能为空！");
        }
        else
        {
            names[i] = a;
            break;
        }
    }
 }
if (players == 2) { names[2] = names[1]; names[1] = ""; }
Console.Clear();


int n = ini.Length;
int locate(int player,int p)
{
    if (pieces[player, p] == 0)
        return -1;
    int start = 0;
    switch (player)
    {
        case 0:
            start = 14;
            break;
        case 1:
            start = 7;
            break;
        case 2:
            start = 65;
            break;
        case 3:
            start = 72;
            break;
    }
    for(int i = 1;i < pieces[player, p]; i++)
    {
        if ((start == 3 && player == 1) || (start == 33 && player == 0) || (start == 46 && player == 2) || (start == 76 && player == 3)) start = b[start].next[1].x;
        else start = b[start].next[0].x;
    }
    return start;
}
int place(int position)
{
    int c = 0;
    for (int i = 0; i < 4; i++)
        for (int j = 0; j < 4; j++)
            if (locate(i, j) == position && pieces[i,j]!=0)
            {
                Console.ForegroundColor = t[i];
                if (Console.ForegroundColor == Console.BackgroundColor)
                    Console.BackgroundColor = ConsoleColor.Black;
                c++;
            }
    if(c==0)return 0;
    return c;
}
int drawMap()
{
    string[] turn = { "黄色", "绿色", "蓝色", "红色" };
    for(int i = 0; i < 4; i++)
    {
        Console.WriteLine("{0}:{1}", turn[i], names[i]);
    }
    int[] counter = { 0, 0 };
    int c = 0;
    for (int i = 0;i < n;i++)
    {
        for(int j = 0;j < ini[i].Length; j++)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            if (ini[i][j] == '4')
            {
                if (counter[0] == 0) Console.BackgroundColor = ConsoleColor.Yellow;
                else if (counter[0] == 1) Console.BackgroundColor = ConsoleColor.Green;
                else if (counter[0] == 2) Console.BackgroundColor = ConsoleColor.Red;
                else Console.BackgroundColor = ConsoleColor.Blue;
                int k = 0;
                int x = counter[0];
                if (x == 2) x = 3;
                else if (x == 3) x = 2;
                for (int l = 0; l < 4; l++) { 
                    if (pieces[x, l] != 0) k++;
                }
                counter[0]++;
                Console.Write(4 - k);
            }
            else {
                
                if (ini[i][j] == 'B') { Console.BackgroundColor = ConsoleColor.Blue; c++; int x = place(c - 1); if (x!=0){ Console.Write(x); }  }
                else if (ini[i][j] == 'R'){ Console.BackgroundColor = ConsoleColor.Red; c++; int x = place(c - 1); if (x != 0) { Console.Write(x); };
                }
                else if (ini[i][j] == 'G'){ Console.BackgroundColor = ConsoleColor.Green; c++; int x = place(c - 1); if (x != 0) { Console.Write(x); };
                }
                else if (ini[i][j] == 'Y'){ Console.BackgroundColor = ConsoleColor.Yellow; c++; int x = place(c - 1);   if (x != 0) { Console.Write(x); };
                }
                else { Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.White; }
                if (ini[i][j] == 'B' || ini[i][j] == 'R' || ini[i][j] == 'G' || ini[i][j] == 'Y') Console.Write(' '); else Console.Write(ini[i][j]);
            }
        }
    }
    Console.ForegroundColor = ConsoleColor.White;


    return 0;
}
void Main()
{    

    int[] stat = new int[80];
    drawMap();
    int cur = 0;
    int end = 0;
    while (true)
    {
        cur = play(cur);
        Console.Clear();
        drawMap();
        for (int i = 0; i < 4; i++)
        {
            if (pieces[i, 0] + pieces[i, 1] + pieces[i, 2] + pieces[i, 3] == 228)
            {
                Console.WriteLine("{0}获胜！游戏结束。", names[i]);
                end = 1;
            }
        }
        if (end == 1) break;
    }
}

bool movable(int player,int roll, int p)
{
    if (pieces[player, p] == 57)
        return false;
    else if (pieces[player, p] == 0)
        return roll >= 5;
    return true;
}


void move(int player,int roll, int p)
{
    if(pieces[player, p] == 0)
    {
        pieces[player, p]++;
        Console.WriteLine("{0}的战机{1}起飞。", names[player],p);
        return;
    }
    for(int i = 0; i < roll; i++) { pieces[player, p] ++;
        if (pieces[player, p] > 57) pieces[player, p] = 57;
        Console.Clear();
        drawMap();
        Thread.Sleep(1000/speed);
    }
    
    collide(player, p);
    
    
    Console.Clear();
    drawMap();
    if (pieces[player, p] == 15)
    {
        Console.WriteLine("同色格，跳跃到下一个同色格！");
        Thread.Sleep(1500 / speed);
        pieces[player, p] += 4;
        collide(player, p);
        Console.Clear();
        drawMap();
        
        Console.WriteLine("进入飞行航线！");
        Thread.Sleep(1500 / speed);
        fly(player, p);
        pieces[player, p] += 12;
        collide(player, p);
        Console.Clear();
        drawMap();

    }
    else if (pieces[player, p] == 19)
    {
        Console.WriteLine("进入飞行航线！");
        Thread.Sleep(1500 / speed);
        fly(player, p);
        pieces[player, p] += 12;
        collide(player, p);
        Console.Clear();
        drawMap();
        Console.WriteLine("同色格，跳跃到下一个同色格！");
        Thread.Sleep(1500 / speed);
        pieces[player, p] += 4;
        collide(player, p);
        Console.Clear();
        drawMap();

    }
    else if (pieces[player, p] % 4 == 3 && pieces[player,p]<50)
    {
        Console.WriteLine("同色格，跳跃到下一个同色格！");
        Thread.Sleep(1500 / speed);
        pieces[player, p] += 4;
        collide(player, p);
        Console.Clear();
        drawMap();
    }
    Console.WriteLine("现在{0}的战机{1}在{2}。",names[player], p, pieces[player,p]);
    //Console.WriteLine("按任意键继续。");
    Thread.Sleep(2000 / speed);
}
void collide(int player,int p)
{
    for (int i = 0; i < 4; i++)
        for (int j = 0; j < 4; j++)
        {
            if (i != player && locate(player, p) == locate(i, j))
            {
                Console.WriteLine("{0}撞到了{1}的飞机，{1}的飞机必须重新起飞！");
                Thread.Sleep(3000 / speed);
                pieces[i, j] = 0;
            }
        }
}
void fly(int player,int p)
{
    int q = (p + 2) % 4;
    for(int i = 0;i<4;i++)
    {
        if (locate(q, i) == b[locate(player, p)].next[1].x)
        {
            Console.WriteLine("{0}撞到了{1}的飞机，{1}的飞机必须重新起飞！");
            pieces[q,i] = 0;
        }
    }
}
int play(int player)
{
    Random x = new Random();
    int n = x.Next(6)+1;
    Console.WriteLine("{0}掷骰结果为{1}。",names[player], n);
    int choice = 0;
    for(int i = 0;i < 4; i++)
    {
        if (pieces[player, i] == 0&&n >= 5)
        {
            Console.WriteLine("{0}：出征战机。", i);
            choice++;
        }
        else if (pieces[player, i] != 57 && pieces[player, i] !=0)
        {
            Console.WriteLine("{0}：移动位置在{1}的战机。", i, pieces[player, i]);
            choice++;
        }
    }
    if (choice == 0) {
        Console.WriteLine("{0}无法移动。", names[player]);
        Console.ReadKey();
        if (players == 2) return (player + 2) % 4;
        return (player + 1) % players;
    }
    int flag = 0;

    while (true)
    {
        flag = 0;
        string a = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(a))
        {
            Console.WriteLine("输入不能为空！");
            continue;
        }
        else { 
        switch (a)
        {
            case "0":
                if(movable(player, n, 0)){
                    Console.WriteLine("{0}选择战机0。", names[player]);
                    move(player, n, 0);
                }
                    else
                    {
                        Console.WriteLine("非法输入！请重新选择。");
                        flag = 1;
                    }
                    break;
            case "1":
                if (movable(player, n, 1))
                {
                    Console.WriteLine("{0}选择战机1。", names[player]);
                    move(player, n, 1);
                }
                    else {
                        Console.WriteLine("非法输入！请重新选择。");
                        flag = 1;
                    }
                        
                    break;

            case "2":
                if (movable(player, n, 2))
                {
                    Console.WriteLine("{0}选择战机2。", names[player]);
                    move(player, n, 2);
                }
                    else
                    {
                        Console.WriteLine("非法输入！请重新选择。");
                        flag = 1;
                    }
                    break;

            case "3":
                if (movable(player, n, 3))
                {
                    Console.WriteLine("{0}选择战机3。", names[player]);
                    move(player, n, 3);
                }
                    else
                    {
                        Console.WriteLine("非法输入！请重新选择。");
                        flag = 1;
                    }
                    break;
            default:
                Console.WriteLine("非法输入！请重新选择。");
                flag = 1;
                break;
        }
        }
        if (flag == 0) break;
    }

    if (n == 6) {
        Console.WriteLine("666！继续行动！");
        Console.ReadKey();
        return player; }
    Console.ReadKey();
    if (players == 2) return (player + 2) % 4;
    return (player+1)%players;
}

while (true)
{
    Main();
    Console.WriteLine("感谢游玩！是否再来一局？y/n");

    string ans = Console.ReadLine();
    if (ans == "n" || ans == "N")
    {
        Console.WriteLine("再见，祝您好运！");
        Console.ReadKey();
        break;
    }
    else if (ans == "Y" || ans == "y") ;
    else Console.WriteLine("输入错误！请重新输入。");
}
