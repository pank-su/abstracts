﻿using System;

namespace PR15
{
    public class Product
    {
        private string _name;
        private double _price;
        private uint _quantity;
        private double _cost;

        public double Cost => _cost;

        public double Price
        {
            get => _price;
            set
            {
                if (value < 0) _price = 0;
                else _price = value;
                _cost = _quantity * _price;
            }
        }
        
        public int Quantity 
        {
            get => (int) _quantity;
            set
            {
                if (value < 0) _quantity = 0;
                else _quantity = (uint) value;
                _cost = _quantity * _price;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value is null || value.Trim() == "") throw new NullReferenceException("У товара должно быть имя.");
                _name = value;
            }
        }

        // Зачем писать три конструктора, когда можно сделать так? Красиво, понятно и мало.
        public Product(string name, float price = 0, int quantity = 1)
        {
            (Name, Price, Quantity) = (name, price, quantity);
        }

        public override string ToString()
        {
            return
                $"Товар {Name}, цена за одну единицу товар {Price}, в количестве {Quantity}. Стоимость: {_cost}";
        }

        public static Product operator ++(Product product)
        {
            product.Quantity++;
            return product;
        }

        public static Product operator --(Product product)
        {
            product.Quantity--;
            return product;
        }

        public static Product operator -(Product product, int n)
        {
            return new Product(product.Name) {Price = product.Price, Quantity = product.Quantity - n};
        }

        // C#, в чём проблема сделать оператор += и -=?
        public static Product operator +(Product product, int n)
        {
            return new Product(product.Name) {Price = product.Price, Quantity = (product.Quantity + n)};
        }
        

        public static bool operator ==(Product p1, Product p2)
        {
            if (p1.Name == p2.Name && Math.Abs(p1.Price - p2.Price) < 0.01) return true;
            return false;
        }

        public void AddToCart()
        {
            Cart.AddProduct(this);
        }

        public void AddQuality(int n)
        {
            Quantity += n;
        }
        
        public static bool operator !=(Product p1, Product p2)
        {
            return !(p1 == p2);
        }
        public void Print()
        {
            Console.WriteLine(this);
        }
    }
}