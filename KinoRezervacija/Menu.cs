﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoRezervacija
{
    abstract class Menu
    {
        public List<IExtra> Food { get; set; }
        public List<IExtra> Drink { get; set; }

        public int FoodCapacity;
        public int DrinkCapacity;

        protected decimal FoodDiscount = 1;
        protected decimal DrinkDiscount = 1;
        public int GetPrice()
        {
            return (int)(Food.Sum(x => x.Price * FoodDiscount) + Drink.Sum(x => x.Price * DrinkDiscount));
        }
        public string GetDescription()
        {
            return string.Join(", ", Food.Select(x => x.Description).ToArray()) + " discount: " + (100 - FoodDiscount * 100) +"%\n"
                + String.Join(", ", Drink.Select(x => x.Description).ToArray()) + " discount: " + (100 - DrinkDiscount * 100) +"%\n" 
                + "Price: " + GetPrice().ToString();
        }

        public int FoodSelectionLeft()
        {
            return FoodCapacity - Food.Count;
        }

        public int DrinkSelectionLeft()
        {
            return DrinkCapacity - Drink.Count;
        }
        
        public int AddFood(IExtra food)
        {
            if (FoodSelectionLeft() == 0) return -1;
            Food.Add(food);
            return FoodSelectionLeft();
        }

        public int AddDrink(IExtra food)
        {
            if (DrinkSelectionLeft() == 0) return -1;
            Drink.Add(food);
            return DrinkSelectionLeft();
        }


        public bool IsFull()
        {
            return FoodSelectionLeft() == 0 && DrinkSelectionLeft() == 0;
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }

    interface IExtra
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public string Description
        {
            get { return Name + " - " + Price; }
        }

    }
    abstract class Addition : IExtra
    {
        private string _name;
        private int _price;

        protected Addition(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name
        {
            get { return FoodDrink.Name + ", " + _name; }
            set { _name = value; }
        }

        public int Price
        {
            get { return _price + FoodDrink.Price; }
            set { _price = value; }
        }

        public IExtra FoodDrink { get; set; }
    }

    class Food : IExtra
    {
        public Food(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return Name + " - " + Price;
        }
    }

    class FoodAddition : Addition
    {
        public FoodAddition(string name, int price, IExtra food) : base(name, price)
        {
            FoodDrink = food;
        }
    }

    class Drink : IExtra
    {
        public Drink(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return Name + " - " + Price;
        }
    } 

    class DrinkAddition : Addition
    {
        public DrinkAddition(string name, int price, IExtra drink) : base(name,price)
        {
            FoodDrink = drink;
        }
    }

    class DefaultMenu : Menu
    {
        public DefaultMenu()
        {
            this.Food = new List<IExtra>();
            this.Drink = new List<IExtra>();
            this.FoodCapacity = 1;
            this.DrinkCapacity = 1;
            this.FoodDiscount = (decimal)0.90;
            this.DrinkDiscount = (decimal)0.90;
        }
    }

    class LoveMenu : Menu
    {
        public LoveMenu()
        {
            this.Food = new List<IExtra>();
            this.Drink = new List<IExtra>();
            this.FoodCapacity = 1;
            this.DrinkCapacity = 2;
            this.FoodDiscount = (decimal)0.95;
            this.DrinkDiscount = (decimal)0.85;
        }
    }

    class FriendMenu : Menu
    {
        public FriendMenu()
        {
            this.Food = new List<IExtra>();
            this.Drink = new List<IExtra>();
            this.FoodCapacity = 5;
            this.DrinkCapacity = 5;
            this.FoodDiscount = (decimal)0.6;
            this.DrinkDiscount = (decimal)0.6;
        }
    }
}
