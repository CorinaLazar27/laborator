
using System.Collections.Generic;
using Models;

namespace Repositories
{

    public static class StudentRepo
    {
        public static List<Student> Students = new List<Student>(){
        new Student() {Id=1, Nume="Lazar", Prenume="Corina", Facultate="AC", AnStudiu=4},
        new Student() {Id=2, Nume="Popescu", Prenume="Bianca", Facultate="ETC", AnStudiu=3},
        new Student() {Id=3, Nume="Ionescu", Prenume="Vasile", Facultate="MEC", AnStudiu=2},
        new Student() {Id=4, Nume="Enescu", Prenume="Madalina", Facultate="AC", AnStudiu=4},
        new Student() {Id=5, Nume="Enache", Prenume="Darius", Facultate="ETC", AnStudiu=1},
        new Student() {Id=6, Nume="Marcu", Prenume="Angela", Facultate="MEC", AnStudiu=4},
        new Student() {Id=7, Nume="Giurgiu", Prenume="Roxana", Facultate="AC", AnStudiu=2},
        };
    }
}