Technologi:
- .Net Core 5.0.12
- MVC
- MSSQL Server min versi 2012

Pastikan ConnectionStrings > DefaultConnection, di dalam appsettings.json sudah di set dengan benar

Database akan ter-create otomatis beserta table-table nya saat aplikasi di running

Data user dummy otomatis terbuat antara lain:
- UserName: admin, password: admin, role: Admin
- UserName: user1, password: user1, role: Penyewa

UI menggunakan bootstrap 5, UI belum 100% selesai

Aplikasi berjalan di url http://localhost:5001
