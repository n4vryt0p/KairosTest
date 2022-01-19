// See https://aka.ms/new-console-template for more information

using KairosConsoleTest;
using System.Globalization;
using System.Text;

Console.WriteLine("Masukan tipe tes B/C/D/E, kosongkan jika ingin keluar, contoh: b");
var typeX = Console.ReadLine()?.ToUpper();

while (!string.IsNullOrEmpty(typeX))
{
    switch (typeX)
    {
        case "B":
            Console.WriteLine($"{Environment.NewLine}Tes {typeX}");
            Console.WriteLine("Silahkan input angka, kosongkan jika ingin skip");
            var angka = Console.ReadLine();
            if (!string.IsNullOrEmpty(angka) && !string.IsNullOrWhiteSpace(angka))
            {
                var tesAngka = angka.Replace(".", string.Empty).Trim();
                if (!long.TryParse(tesAngka, out long value))
                    Console.WriteLine($"{Environment.NewLine}Invalid angka {angka}");
                else
                {
                    Console.WriteLine($"{Environment.NewLine}Hasil:");
                    for (int i = 0; i < tesAngka.Length; i++)
                    {
                        var log2 = Extensionss.Fill(tesAngka[i].ToString(), tesAngka.Length - i, "0");
                        Console.WriteLine($"{log2}");
                    }
                }
            }

            Console.WriteLine($"{Environment.NewLine}Selesai, masukan tipe tes B/C/D/E, kosongkan jika ingin keluar");
            typeX = Console.ReadLine()?.ToUpper();
            break;
        case "C":
            Console.WriteLine($"{Environment.NewLine}Tes {typeX}");
            Console.WriteLine("Silahkan input kalimat, kosongkan jika ingin skip");
            var kalimat = Console.ReadLine();
            if (!string.IsNullOrEmpty(kalimat) && !string.IsNullOrWhiteSpace(kalimat))
            {
                var tesKalimat = kalimat.Replace(" ", string.Empty).Trim();
                List<char> datalist = new();
                datalist.AddRange(tesKalimat);
                var b = datalist.GroupBy(x => x).Select(x => new {
                    Count = x.Count(),
                    Char = x.First()
                }).ToList();

                Console.WriteLine($"{Environment.NewLine}Hasil:");
                foreach (var item in b)
                    Console.WriteLine($"{item.Char} - {item.Count}");
            }

            Console.WriteLine($"{Environment.NewLine}Selesai, masukan tipe tes B/C/D/E, kosongkan jika ingin keluar");
            typeX = Console.ReadLine()?.ToUpper();
            break;
        case "D":
            Console.WriteLine($"{Environment.NewLine}Tes {typeX}");
            Console.WriteLine("Silahkan input jumlah, kosongkan jika ingin skip");
            var jmlh = Console.ReadLine();
            if (!string.IsNullOrEmpty(jmlh) && !string.IsNullOrWhiteSpace(jmlh))
            {
                if (!long.TryParse(jmlh, out long value))
                    Console.WriteLine($"{Environment.NewLine}Invalid jumlah {jmlh}");
                else
                {
                    List<string> datalist = new();
                    for (int i = 1; i <= value; i++)
                    {
                        if (i > 6 && (i % 5 == 0))
                            datalist.Add("IDIC");
                        else if (i > 6 && (i % 6 == 0))
                            datalist.Add("LPS");
                        else
                            datalist.Add(i.ToString());
                    }

                    Console.WriteLine($"{Environment.NewLine}Hasil:");
                    Console.WriteLine($"{string.Join(" ", datalist)}");
                }
            }

            Console.WriteLine($"{Environment.NewLine}Selesai, masukan tipe tes B/C/D/E, kosongkan jika ingin keluar");
            typeX = Console.ReadLine()?.ToUpper();
            break;
        case "E":
            Console.WriteLine($"{Environment.NewLine}Tes {typeX}");
            Console.WriteLine("Silahkan input kalimat, kosongkan jika ingin skip");
            var kal = Console.ReadLine();
            if (!string.IsNullOrEmpty(kal) && !string.IsNullOrWhiteSpace(kal))
            {
                var kalim = kal.ToLower();
                var biasa = $"{char.ToUpper(kalim[0])}{kalim[1..]}";
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                kalim = textInfo.ToTitleCase(kalim);

                Console.WriteLine($"{Environment.NewLine}Hasil:");
                Console.WriteLine($"Format Judul: {kalim}");
                Console.WriteLine($"Format Biasa: {biasa}");
            }


            Console.WriteLine($"{Environment.NewLine}Selesai, masukan tipe tes B/C/D/E, kosongkan jika ingin keluar");
            typeX = Console.ReadLine()?.ToUpper();
            break;
        default:
            Console.WriteLine($"{Environment.NewLine}Tidak ada tipe tes {typeX}, silahkan input kembali, kosongkan jika ingin keluar");
            typeX = Console.ReadLine()?.ToUpper();
            break;
    }
}

Console.Write($"{Environment.NewLine}Press any key to exit...");
Console.ReadKey(true);