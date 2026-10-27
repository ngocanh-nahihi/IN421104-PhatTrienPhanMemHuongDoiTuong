    User Story – Khách hàng
    US-01 Khách hàng: tạo đơn hàng (đặt hoa)
        •	1. Giỏ hàng có sản phẩm -> đặt hàng -> tạo Order (Pending = chờ thanh toán)
        •	2. Giỏ hàng trống -> đặt hàng -> báo lỗi
    US-02 Khách hàng: thanh toán bằng mã QR
        •	1. Đơn hàng Pending -> thanh toán -> sinh mã QR, trạng thái chuyển thành Paid (đã thanh toán)
        •	2. Đơn hàng Cancelled -> thanh toán -> bị từ chối

    User Story – Nhân viên 
    US-03 Nhân viên: xuất hóa đơn bán hàng
        •	1. Đơn hàng Paid -> in hóa đơn -> tạo file text invoice chứa chi tiết Order
        •	2. Đơn hàng Pending -> in hóa đơn -> bị từ chối (do chưa thanh toán)
    US-04 Nhân viên: xác nhận giao hàng
        •	1. Đơn hàng Paid -> giao hàng -> đổi trạng thái thành Delivering (đang giao)
        •	2. Đơn hàng Pending / Cancelled -> giao hàng -> báo lỗi
    User Story – Chủ cửa hàng 
    
    US-05 Chủ cửa hàng: xem báo cáo doanh thu
        •	1. Chọn tháng có đơn hàng Paid -> xem báo cáo -> thấy tổng doanh thu và số lượng đơn
        •	2. Liệt kê từng đơn: hiển thị chi tiết các đơn hàng đã thanh toán thành công

```mermaid
classDiagram
    class Customer {
        +Guid Id
        +string Name
        +string Phone
        +string Email
        +DateTime JoinedAt
        +bool IsActive
        +Create(name, phone, email) Customer
        +Deactivate() void
    }

    class Flower {
        +Guid Id
        +string Name
        +decimal Price
        +int StockQuantity
        +Create(name, price, stock) Flower
        +HasStock(quantity) bool
        +ReduceStock(quantity) void
    }

    class Order {
        +Guid Id
        +Guid CustomerId
        +DateTime OrderDate
        +decimal TotalAmount
        +OrderStatus Status
        +Create(customerId, totalAmount) Order
        +MarkAsPaid() void
        +SetDelivering() void
        +Cancel() void
    }

    class Payment {
        +Guid Id
        +Guid OrderId
        +DateTime PaidAt
        +string Method
        +decimal Amount
        +Create(order, method) Payment
    }

    class Invoice {
        +Guid Id
        +Guid OrderId
        +DateTime IssuedAt
        +string StaffName
        +Create(order, staffName) Invoice
        +GetDetails() string
    }

    Customer "1" --> "*" Order : đặt hàng (FK CustomerId)
    Order "*" --> "*" Flower : gồm các sản phẩm
    Order "1" --> "0..1" Payment : thanh toán (FK OrderId)
    Order "1" --> "0..1" Invoice : xuất hoá đơn (FK OrderId)
```
