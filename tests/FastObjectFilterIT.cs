using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FastObjectFilter.Tests
{
    [TestClass]
    public class FastObjectFilterIT
    {
        private enum Crocodiles
        {
            Saltwater,
            Plastic
        }

        private class DataObject
        {
            public string Forename { get; set; }
            public string Surname { get; set; }
            public DateTime Dob { get; set; }

            public bool LikesCats { get; set; }

            public string? NullString { get; set; }

            public short NumberOfCrocodiles { get; set; }

            public Crocodiles FavouriteCrocodile { get; set; }

            public DataObject(string forename, string surname, DateTime dob, bool likesCats, short numberOfCrocodiles, Crocodiles favouriteCrocodile)
            {
                Forename = forename;
                Surname = surname;
                Dob = dob;
                LikesCats = likesCats;
                NullString = null;
                NumberOfCrocodiles = numberOfCrocodiles;
                FavouriteCrocodile = favouriteCrocodile;
            }
        }

        [TestMethod]
        [DataRow("Forename == \"Steve\"")]
        [DataRow("Dob.Year == 1962")]
        [DataRow("LikesCats == true")]
        [DataRow("NullString == null")]
        [DataRow("NumberOfCrocodiles == 10")]
        [DataRow("NumberOfCrocodiles < 11")]
        [DataRow("NumberOfCrocodiles > 9")]
        [DataRow("NumberOfCrocodiles <= 11")]
        [DataRow("NumberOfCrocodiles >= 9")]
        [DataRow("NumberOfCrocodiles <= 10")]
        [DataRow("NumberOfCrocodiles >= 10")]
        [DataRow("NumberOfCrocodiles != 7")]
        [DataRow("FavouriteCrocodile == Crocodiles.Saltwater")]
        [DataRow("Dob.Year == 1962 || LikesCats == true")]
        [DataRow("Dob.Year == 1963 || LikesCats == true")]
        [DataRow("Dob.Year == 1962 || LikesCats == false")]
        [DataRow("Dob.Year == 1962 && LikesCats == true")]
        public void TestMatchingObjectIsSelected(string rule)
        {
            // GIVEN a valid filter string
            // WHEN compiled
            Func<DataObject, bool> filter = new FastObjectFilterCompiler().Compile<DataObject>(rule);

            // THEN the filter is not null
            Assert.IsNotNull(filter);

            // GIVEN an data object to format
            DataObject data = new DataObject("Steve", "Irwin", new DateTime(1962, 9, 22), true, 10, Crocodiles.Saltwater);

            // WHEN the object is filtered
            bool result = filter(data);

            // THEN the filter asserts a match
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("Forename == \"Jamie\"")]
        [DataRow("LikesCats == false")]
        [DataRow("Forename == null")]
        [DataRow("NumberOfCrocodiles == 12")]
        [DataRow("NumberOfCrocodiles < 10")]
        [DataRow("NumberOfCrocodiles > 10")]
        [DataRow("NumberOfCrocodiles <= 9")]
        [DataRow("NumberOfCrocodiles >= 11")]
        [DataRow("NumberOfCrocodiles != 10")]
        [DataRow("FavouriteCrocodile == Crocodiles.Plastic")]
        [DataRow("Dob.Year == 1963 && LikesCats == true")]
        [DataRow("Dob.Year == 1962 && LikesCats == false")]
        [DataRow("Dob.Year == 1963 && LikesCats == false")]
        [DataRow("Dob.Year == 1963 || LikesCats == false")]
        public void TestNonMatchingObjectIsNotSelected(string rule)
        {
            // GIVEN a valid filter string
            // WHEN compiled
            Func<DataObject, bool> filter = new FastObjectFilterCompiler().Compile<DataObject>(rule);

            // THEN the filter is not null
            Assert.IsNotNull(filter);

            // GIVEN an data object to format
            DataObject data = new DataObject("Steve", "Irwin", new DateTime(1962, 9, 22), true, 10, Crocodiles.Saltwater);

            // WHEN the object is filtered
            bool result = filter(data);

            // THEN the filter asserts there was not a match
            Assert.IsFalse(result);
        }
    }
}
