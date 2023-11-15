﻿namespace webapi.core.Entities
{
    public class FieldEntity : BaseEntity
    {
        public int Multiplier { get; set; }

        public int UserId {  get; set; }
        public UserEntity User { get; set; }

        public List<OilPumpEntity> OilPumps { get; set; } = new();
    }
}