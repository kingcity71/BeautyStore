﻿using BeautyStore.Entities.Abstract;
using System;

namespace BeautyStore.Entities
{
    public class Store : Entity
    {
        public Guid ProductId { get; set; }
        public Guid BranchId { get; set; }
        public int Count{ get; set; }
    }
}