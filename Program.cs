using System.Text;
using EduHub.Ch01.Domain;

Console.OutputEncoding = Encoding.UTF8;

// Bước 1: Tạo CLB
var club = Club.Create("Tech Club", "Câu lạc bộ Công nghệ Thông tin ĐH Đồng Tháp");
Console.WriteLine($"1. Đã tạo CLB: {club.Name}");

// Bước 2: Thêm 3 thành viên
var m1 = Member.Create("Huỳnh Võ Minh Đoàn", "doan@student.dthu.edu.vn", "0024419249");
var m2 = Member.Create("Phạm Ngọc Anh", "anh@student.dthu.edu.vn", "0024419250");
var m3 = Member.Create("Nguyễn Văn B", "b@student.dthu.edu.vn", "0024419251");

club.AddMember(m1);
club.AddMember(m2);
club.AddMember(m3);
Console.WriteLine($"2. Đã thêm {club.MemberCount()} thành viên vào CLB.");

// Bước 3: Tạo sự kiện
var ev = Event.Create(club.Id, "Workshop OOP & Domain Modeling", new DateTime(2026, 10, 25), capacity: 50, isOnline: false);
Console.WriteLine($"3. Đã tạo sự kiện: {ev.Title} (Sức chứa: {ev.Capacity})");

// Bước 4: Đăng ký tham gia (Pending)
var registrations = new List<Registration>
{
    Registration.Create(m1.Id, ev.Id),
    Registration.Create(m2.Id, ev.Id),
    Registration.Create(m3.Id, ev.Id)
};
Console.WriteLine($"4. Có {registrations.Count} phiếu đăng ký mới (Trạng thái: Pending).");

// Bước 5: Xác nhận phiếu đăng ký (Confirmed) cho SV 1 và SV 2, SV 3 huỷ phiếu
registrations[0].Confirm();
registrations[1].Confirm();
registrations[2].Cancel();
Console.WriteLine("5. Đã xác nhận 2 phiếu (Confirmed) và huỷ 1 phiếu (Cancelled).");

// Bước 6: Check-in qua QR cho SV 1
var checkIns = new List<CheckIn>();
var checkIn1 = CheckIn.Create(registrations[0], CheckInMethod.QrCode);
checkIns.Add(checkIn1);
Console.WriteLine($"6. Điểm danh thành công: {checkIn1.Summary()}");

// Bước 7: In báo cáo tham dự
Console.WriteLine("\n=== 7. BÁO CÁO THAM DỰ SỰ KIỆN ===");
Console.WriteLine($"Sự kiện: {ev.Title} | Đã đăng ký: {registrations.Count}/{ev.Capacity} | Đã Check-in: {checkIns.Count}");
foreach (var reg in registrations)
{
    var member = club.Members.First(m => m.Id == reg.MemberId);
    bool isCheckedIn = checkIns.Any(c => c.RegistrationId == reg.Id);
    Console.WriteLine($" - {member.Name} ({member.StudentCode}) | Phiếu: {reg.Status} | Check-in: {(isCheckedIn ? "Đã điểm danh" : "Chưa")}");
}

// Bước 8: Thử vi phạm luật nghiệp vụ (Theo Slide 66)
Console.WriteLine("\n=== 8. KIỂM TRA VI PHẠM LUẬT NGHIỆP VỤ ===");
registrations[1].Cancel();
try
{
    registrations[1].Confirm(); // Thử xác nhận lại phiếu đã huỷ
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"[Bắt lỗi thành công]: {ex.Message}");
}
