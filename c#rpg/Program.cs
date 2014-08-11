using System;

namespace crpg
{
	//物品 
	class Item
	{
		public int addatk;
		public int addhp;
		public string name;

		public Item(string name,int addatk,int addhp)
		{
			this.name = name;
			this.addatk = addatk;
			this.addhp = addhp;
			Console.Write ("{0} created!\n", this.name);
		}

	}

	//野怪
	class Mob
	{
		public int hp;
		protected int atk;
		//protected Item award;
		public int score;
		public string name;

		//public Mob(string name,int hp,int atk,Item award)
		public Mob(string name)
		{
			this.name = name;
			//Console.Write ("{0} created!\n", this.name);
		}

		public void TakeDamage(int atk)
		{
			this.hp -= atk;
			Console.Write ("{0} lost {1} hp.\n", this.name,atk);
			if (this.hp <= 0) {
				Console.Write ("{0} died!\n",this.name);
				//GameFuction.RandomAward ();
			}
		}

		//玩家和野怪类都有攻击方法，此处可优化
		public void Atk(Player target)
		{
			Console.Write ("{0} attacked {1} !\n", this.name,target.name);
			target.TakeDamage (this.atk);
		}

		public void Debug()
		{
			Console.Write ("{0} created!", this.name);
		}
	}

	//野怪种类
	class Goblin :Mob
	{
		public Goblin(string name):base("")
		{
			this.name = name;
			this.hp = 7;
			this.atk = 3;
			this.score = 3;
			//this.Debug();
		}
	}

	class Slime:Mob
	{
		public Slime(string name):base("")
		{
			this.name = name;
			this.hp = 4;
			this.atk = 1;
			this.score = 1;
			//this.Debug();
		}
	}

	class Troll:Mob
	{
		public Troll (string name):base("")
		{
			this.name = name;
			this.hp = 10;
			this.atk = 3;
			this.score = 5;
			//this.Debug();
		}

	}





	//玩家
	class Player
	{
		public int hp;
		public int atk;
		public Item equip;
		public int score;

		public string name;

		public Player(string name)
		{
			this.name = name;
			this.hp = 20;
			//this.equip = equip;
			this.score = 0;
			this.atk = 3;
			//Console.Write ("{0} created!\n", this.name);
		}
	

		public void Atk(Mob target)
		{
			Console.Write ("{0} attacked {1} !\n", this.name,target.name);
			target.TakeDamage (this.atk);
		}

		public void Equip(Item item)
		{
			this.hp += item.addhp;
			this.atk += item.addatk;
			this.equip = item;
			Console.Write ("{0} equiped {1}.\n", this.name,this.equip.name);
		}

		public void TakeDamage(int atk)
		{
			this.hp -= atk;
			Console.Write ("{0} lost {1} hp.\n", this.name,atk);
			//if (this.hp <= 0) {
				//Console.Write ("You died! Game Over!");
			//}
		}


	}

    class GameFuction
	{
		public static Mob RandomEngage()
		{
			Mob mob;
			Random r = new Random ();
			int i = r.Next (9);
			if (i < 3) {
				mob = new Goblin ("goblin");
			}
			else if (i<6) {
				mob = new Troll ("troll");
			}
			else {
				mob = new Slime ("slime");
			}
			Console.Write ("A {0} attacked you!\n", mob.name);
			return mob;
		}

		//public static void RandomAward(Player player)

		public static void Initialize()
		{
			Console.Write ("Please input your name:");
			string name = Console.ReadLine ();
			Player player = new Player (name);

			if (player != null) {
				Console.Write ("Welcome {0} to the World of Omega!\n", player.name);
				GameFuction.Menu (player);
			}
		}

		public static void Menu(Player player)
		{
			Console.Write("{0}\nHP:{1}\nATK:{2}\nScore:{3}\nWhat are you going to do:",player.name,player.hp,player.atk,player.score);
			string todo = Console.ReadLine ();

			switch (todo) {
			case "adv":
				GameFuction.ToBattle (player, GameFuction.RandomEngage ());
				break;
			}
		}

		public static void Battle(int flag,Player player,Mob mob)
		{
			if (player.hp <= 0 || mob.hp <= 0) {
				if (player.hp <= 0) {
					GameFuction.GameOver (player);
				} else {
					Console.Write ("You killed {0}\n", mob.name);
					player.score += mob.score;
					GameFuction.Menu (player);
				}
			} else {
				if (flag == 0) {
					player.Atk (mob);
					flag = 1;
					Battle (flag,player, mob);
				} else {
					mob.Atk (player);
					flag = 0;
					Battle (flag,player, mob);
				}
			}
		}

		public static void ToBattle(Player player,Mob mob)
		{
			int flag = 0;
			Battle (flag, player, mob);
		}
	


		public static void GameOver(Player player)
		{
			Console.Write("You died!\nGame Over!\nYour score:{0}\n",player.score);
		}
			

	}


	class MainClass
	{
		public static void Main (string[] args)
		{
			GameFuction.Initialize ();
		}
	}
}
