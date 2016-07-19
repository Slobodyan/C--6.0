using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using static System.Math;

namespace NewInCSharp6 {
	class Program {

		// 1. Інтерполяція стрічок
		//Раніше
		public string OldYearFormat = string.Format("Today is {0} year", DateTime.Now.Year);
		//Зараз
		public string NewYearFormat = $"Today is {DateTime.Now.Year} year";



		// 2. Ініціалізація авто-властивостей, значення по замовчуванню
		//Раніше
		public int Id { get; set; }
		public bool IsOwner { get; set; }
		//Зараз
		public int NewId { get; set; }
		public bool NewIsOwner { get; set; } = true;

		public class Example {
			public List<string> Values { get; } = new List<string>();
		}



		// 3. Властивості і методи з expression body
		//Раніше
		public int nextYear {
			get { return DateTime.Now.Year + 1; }
		}
		public string printNextYear() {
			return string.Format("Next year number will be {0}", DateTime.Now.Year + 1);
		}
		public int calculateNextYear(int currentYearNumber) {
			return currentYearNumber + 1;
		}
		//Зараз
		public int NextYear => DateTime.Now.Year + 1;
		public string PrintNextYear() => $"Next year number will be {DateTime.Now.Year + 1}";
		public int CalculateNextYear(int currentYearNumber) => currentYearNumber + 1;



		// 4. Оператор?.
		public class Pigeon {
			public int Id { get; set; }
			public string RingNumber { get; set; }
			public int BirthYear { get; set; }
		}
		public class Owner {
			public string Name { get; set; }
			public ICollection<Pigeon> Pigeons { get; set; }
		}
		public class Organization {
			public string Name { get; set; }
			public DateTime CreatedTime { get; set; }
			public List<Owner> Owners;
		}
		//Раніше
		public class Test1 {
			public Organization org = new Organization();

			public int GetOwnerPigeonsCount(string ownerName) {
				int count = 0;

				if (org.Owners != null &&
					org.Owners.FirstOrDefault(ow => ow.Name == ownerName) != null &&
					org.Owners.FirstOrDefault(ow => ow.Name == ownerName).Pigeons != null) {
					count = org.Owners.FirstOrDefault(ow => ow.Name == ownerName).Pigeons.Count;
				}
				return count;
			}
		}
		//Зараз
		public class Test2 {
			public Organization org = new Organization();

			public int GetOwnerPigeonsCount(string ownerName) => org.Owners?.FirstOrDefault(ow => ow.Name == ownerName)?.Pigeons.Count ?? 0;
		}



		// 5. Імпорт членів статичних типів в простір імен
		//Раніше
		public double CalculateSqrtOld(double value) {
			return Math.Sqrt(value);
		}

		//Зараз
		public double CalculateSqrtNew(double value) => Sqrt(value);



		static void Main(string[] args) {
			// 6. Оператор nameof - дозволяє отримати ім’я класу чи властивості
			Pigeon pigeon = new Pigeon() {
				BirthYear = 2013,
				Id = 1,
				RingNumber = "UA 781234"
			};

			Console.WriteLine(pigeon.RingNumber); // return UA 781234
			Console.WriteLine(nameof(pigeon.RingNumber)); //return RingNumber
			Console.WriteLine(nameof(Pigeon.RingNumber)); //return RingNumber



			// 7. Ініціалізація індексів – дозволяє вказувати індекси при ініціалізації
			var pigeonList = new List<Pigeon> {
				[3] = new Pigeon(),
				[5] = new Pigeon()
			};

			var dictionary = new Dictionary<int, string> {
				[2] = "two",
				[5] = "five"
			};



			// 8. Фільтри виключень - додають умови, при яких спрацьовує блок catch()
			Owner owner = new Owner() {
				Name = "Andriy",
				Pigeons = null
			};

			try {
				Console.WriteLine(owner.Pigeons.Count);
			} catch (Exception ex) when (ex is NullReferenceException) {
				Console.WriteLine(nameof(NullReferenceException));
			}

			Console.ReadKey();
		}


		// 9. Await в блоках catch/finally
		async void SomeMethod() {
			WebClient wc = new WebClient();
			try {
				string result = wc.DownloadString(new Uri("http://rpi.in.ua"));
			} catch {
				await wc.DownloadDataTaskAsync(new Uri("http://rpi.in.ua"));
			} finally {
				wc.CancelAsync();
			}
		}
	}
}
