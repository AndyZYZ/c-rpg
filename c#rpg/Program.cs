using System;
using System.Collections.Generic;

namespace crpg
{
	//游戏角色类
	class Creature
	{
		public string name;

		public int hp;
		public int maxatk;
		public int minatk;

		public void Atk(Creature target)
		{
			Console.Write ("{0} attacked {1} !\n", this.name,target.name);
			Random r = new Random ();
			int damage = r.Next (this.minatk, this.maxatk + 1);
			target.TakeDamage (damage);
		}

		public void TakeDamage(int atk)
		{
			this.hp -= atk;
			Console.Write ("{0} lost {1} hp.\n", this.name,atk);
		}
	}
		

	//野怪
	class Mob:Creature
	{
		public int score;
		public Item award;

		//public Mob(string name,int hp,int atk,Item award)
		public Mob(string name)
		{
			this.name = name;
			//Console.Write ("{0} created!\n", this.name);
		}
			
		public void Debug()
		{
			Console.Write ("{0} created!", this.name);
		}

		public void DeathAward(Player player)
		{
			player.inventorylist.Add (this.award);
		}

	}

	//野怪种类
	class Goblin :Mob
	{
		public Goblin(string name):base("")
		{
			this.name = name;
			this.hp = 70;
			this.maxatk = 20;
			this.minatk = 10;
			this.score = 3;
			//this.Debug();
		}
	}

	class Slime:Mob
	{
		public Slime(string name):base("")
		{
			this.name = name;
			this.hp = 40;
			this.maxatk = 15;
			this.minatk = 5;
			this.score = 10;
			//this.Debug();
		}
	}

	class Troll:Mob
	{
		public Troll (string name):base("")
		{
			this.name = name;
			this.hp = 100;
			this.maxatk = 25;
			this.minatk = 15;
			this.score = 5;
			this.award = new HealthPotion ("health potion");
			//this.Debug();
		}

	}


	//玩家
	class Player:Creature
	{
		public Item equip;
		public int score;
		public List<Item> inventorylist = new List<Item>();
		//protected Item[] inventory = inventorylist.ToArray();
		


		public Player(string name)
		{
			this.name = name;
			this.hp = 200;
			//this.equip = equip;
			this.score = 0;
			this.minatk = 15;
			this.maxatk = 30;
			//Console.Write ("{0} created!\n", this.name);
		}

		/*public void Equip(Item item)
		{
			this.hp += item.addhp;
			this.atk += item.addatk;
			this.equip = item;
			Console.Write ("{0} equiped {1}.\n", this.name, this.equip.name);
		}*/
	}


	//物品 
	class Item
	{
		public int addatk;
		public int addhp;
		public string name;
	}

	class HealthPotion:Item
	{
		public HealthPotion(string name)
		{
			this.name = name;
			this.addhp = 20;
			this.addatk = 0;
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
			Console.Write("{0}\nHP:{1}\nATK:{2}-{3}\nScore:{4}\nWhat are you going to do:",player.name,player.hp,player.minatk,player.maxatk,player.score);
			string todo = Console.ReadLine ();

			switch (todo) {
			case "adv":
				GameFuction.ToBattle (player, GameFuction.RandomEngage ());
				break;
			case "itm":
				GameFuction.ShowItem (player);
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
					if (mob.award != null) {
						mob.DeathAward (player);
						Console.Write ("You've gained a {0}.\n", mob.award.name);
					}
					player.score += mob.score;
					GameFuction.Menu (player);
				}
			} else {
				if (flag == 0) {
					player.Atk (mob);
					flag = 1;
					System.Threading.Thread.Sleep (1000);
					Battle (flag,player, mob);
				} else {
					mob.Atk (player);
					flag = 0;
					System.Threading.Thread.Sleep (1000);
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

		public static void ShowItem (Player player)
		{
			Item[] items = player.inventorylist.ToArray();
			for (int i = 0;i<items.Length;i++){
				Console.Write (items[i].name);
			}
			Console.Write("\n");
			Menu (player);
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
