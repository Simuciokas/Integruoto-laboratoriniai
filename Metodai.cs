namespace ConsoleApp2
{
	public class Metodai
		{
			public void CreateFile() {
				var duomenys = new List<string>{
					"Vardas Pavardė ND1 ND2 ND3 ND4 ND5 Egzaminas",
					"Rimas Kurtinaitis 8 9 10 6 10 9",
					"Arvydas Sabonis 7 10 8 5 4 6"};
				using (System.IO.StreamWriter file = 
					   new System.IO.StreamWriter(@"kursiokai.txt"))
				{
					foreach (string line in duomenys)
						file.WriteLine(line);
				}
			}
		
			public void GenerateFiles() {
				Random rnd = new Random();
				for (int i = 1; i < 6; i++) {
					using (System.IO.StreamWriter file =
						   new System.IO.StreamWriter(@"kursiokai"+i+".txt"))
					{
						file.WriteLine("Vardas Pavardė ND1 ND2 ND3 ND4 ND5 Egzaminas");
						for (int j = 1; j <= Math.Pow(10,i); j++) {
							string vardas = "Vardas"+j.ToString();
							string pavarde = "Pavarde"+j;
							int egz = rnd.Next(1,11);
							double[] ndBalai = new double[5];
							for (int k = 0; k < 5; k++)
								ndBalai[k] = rnd.Next(1,11);
							Studentas studentas = new Studentas(vardas, pavarde, ndBalai, egz);
							file.WriteLine(studentas);
						}
						Console.WriteLine("Sukurtas failas " + "kursiokai" + i + ".txt su " + Math.Pow(10,i) + " įrašų");
					}
				}
			}
			
			public List<Studentas> ReadFromFile() {
				var studentai = new List<Studentas>();
				string line, vardas = "", pavarde = "";
				var ndBalai = new List<double>();
				int egz = 0;
				try {
					using (System.IO.StreamReader file = 
						   new System.IO.StreamReader(@"kursiokai.txt"))
					{
						line = file.ReadLine();
						while((line = file.ReadLine()) != null)  
						{
							string[] duomenys = line.Split(' ');
							for (int i = 0; i < duomenys.Length; i++) {
								if (i == 0)
									vardas = duomenys[i];
								else if (i == 1)
									pavarde = duomenys[i];
								else if (i == (duomenys.Length - 1)) {
									try {
										egz = Convert.ToInt32(duomenys[i]);
									}
									catch (Exception e)
									{
										Console.WriteLine(e.StackTrace);
										Console.WriteLine("Nepavyko konvertuoti '" + duomenys[i] + "' į Int. Nustatomas egzamino balas į 1");
										egz = 1;
									}
								}
								else {
									try {
										ndBalai.Add(Convert.ToDouble(duomenys[i]));
									}
									catch (Exception e)
									{
										Console.WriteLine(e.StackTrace);
										Console.WriteLine("Nepavyko konvertuoti '" + duomenys[i] + "' į Double. Pridedamas namų darbo balas 1");
										ndBalai.Add(1);
									}
								}
							}
							studentai.Add(new Studentas(vardas, pavarde, ndBalai.ToArray(), egz));
						}
					}
				}
				catch (IOException e)
				{
					Console.WriteLine(e.ToString());
					Console.WriteLine("Įvyko klaida su failu!");
				}
				return studentai;
			}
		}
}