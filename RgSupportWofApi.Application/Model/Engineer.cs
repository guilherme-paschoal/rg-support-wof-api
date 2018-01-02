using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RgSupportWofApi.Application.Model
{
    public class Engineer
    {
        public Engineer() 
        { 
            //Shifts = new List<Shift>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        //public List<Shift> Shifts { get; private set; }

        //public void AddShift(int shiftOrder) {
        //    AddShift(DateTime.Now, shiftOrder);
        //}

        //public void AddShift(DateTime date, int shiftOrder) 
        //{
        //    Shifts.Add(new Shift{
        //        Date = date,
        //        ShiftOrder = shiftOrder
        //    });
        //}

        //public Shift GetLastShift() {
        //    this.Shifts.Sort((x, y) => Data.Co);
        //}
     
        //Sort shift dates descending
        //engineers.ForEach(e=>e.Shifts.Sort((x, y) => y.Date.CompareTo(x.Date)));

    }
}
