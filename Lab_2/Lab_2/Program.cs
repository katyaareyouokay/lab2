using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    class GameObject
    {
        protected int id;
        protected internal string name;
        protected int x;
        protected int y;

        public GameObject(int id, string name, int x, int y)
        {
            this.id = id;
            this.name = name;
            this.x = x;
            this.y = y;
        }
        public int getId() { return id; }
        public string getName() { return name; }
        public int getX() { return x; }
        public int getY() { return y; }
    }
    class Unit : GameObject
    {
        private bool alive;
        private float hp;

        public Unit(int id, string name, int x, int y, bool alive, float hp) : base(id, name, x, y)
        {
            this.alive = alive;
            this.hp = hp;
        }
        public bool isAlive() { return alive; }
        public float getHp() { return hp; }
        public void ReceiveDamage(float damage)
        {
            if (damage < 0) { return; }
            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;
                alive = false;
            }
            Console.WriteLine($"-{damage}hp");
        }
    }
    interface Attacker
    {
        void Attack(Unit unit);
    }

    interface Moveable
    {
        void Move(int newX, int newY);
    }
    class Archer : Unit, Moveable, Attacker
    {
        private int power;
        public Archer(int id, string name, int x, int y, bool alive, float hp, int power) : base(id, name, x, y, alive, hp)
        {
            this.power = power;
        }

        public void Attack(Unit unit)
        {
            unit.ReceiveDamage(power);
            Console.WriteLine($"{name} attacked {unit.name}");
        }

        public void Move(int newX, int newY)
        {
            x = newX;
            y = newY;
        }



    }
    class Building : GameObject
    {
        private bool built;

        public Building(int id, string name, int x, int y, bool built) : base(id, name, x, y)
        {
            this.built = built;
        }
        public bool isBuilt() { return built; }
    }
    class Fort : Building
    {
        private int power;
        public Fort(int id, string name, int x, int y, bool built, int power) : base(id, name, x, y, built)
        {
            this.power = power;
        }

    }
    class MobileHouse : Building, Moveable
    {
        public MobileHouse(int id, string name, int x, int y, bool built) : base(id, name, x, y, built)
        {
        }
        public void Move(int newX, int newY)
        {
            x = newX;
            y = newY;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем лучника (Archer)
            Archer archer = new Archer(1, "Лучник", 0, 0, true, 100, 20);

            // Создаем юнита (Unit)
            Unit enemy = new Unit(2, "Враг", 5, 5, true, 50);

            // Создаем форт (Fort)
            Fort fort = new Fort(3, "Крепость", 10, 10, true, 40);

            // Создаем мобильный дом (MobileHouse)
            MobileHouse mobileHouse = new MobileHouse(4, "Мобильный дом", 0, 0, true);

            // Проверяем передвижение лучника
            Console.WriteLine($"{archer.getName()} был на позиции ({archer.getX()}, {archer.getY()})");
            archer.Move(3, 3);
            Console.WriteLine($"{archer.getName()} переместился на позицию ({archer.getX()}, {archer.getY()})");

            // Лучник атакует врага
            Console.WriteLine($"HP врага до атаки: {enemy.getHp()}");
            archer.Attack(enemy);
            Console.WriteLine($"HP врага после атаки: {enemy.getHp()}");

            // Проверяем передвижение мобильного дома
            Console.WriteLine($"{mobileHouse.getName()} был на позиции ({mobileHouse.getX()}, {mobileHouse.getY()})");
            mobileHouse.Move(8, 8);
            Console.WriteLine($"{mobileHouse.getName()} переместился на позицию ({mobileHouse.getX()}, {mobileHouse.getY()})");

            // Проверяем статус юнита (враг мертв или жив)
            if (enemy.isAlive())
            {
                Console.WriteLine($"{enemy.getName()} еще жив.");
            }
            else
            {
                Console.WriteLine($"{enemy.getName()} мертв.");
            }

            // Проверяем состояние крепости
            Console.WriteLine($"{fort.getName()} на позиции ({fort.getX()}, {fort.getY()}), построена: {fort.isBuilt()}");
            Console.ReadKey();

        }
    }
}

