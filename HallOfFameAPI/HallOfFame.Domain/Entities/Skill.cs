﻿namespace HallOfFame.Domain.Entities
{
    public class Skill : BaseEntity
    {
        public byte Level { get; set; }
        public List<Person> Persons { get; set; }
    }
}
