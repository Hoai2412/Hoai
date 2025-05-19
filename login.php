<?php
// Thiết lập thông tin kết nối đến database
$servername = "database-server-lab7.cocgl5wbv5ga.ap-southeast-1.rds.amazonaws.com";
$username_db = "admin";
$password_db = "hoai123";
$dbname = "myDB";

// Tạo kết nối
$conn = new mysqli($servername, $username_db, $password_db, $dbname);

// Kiểm tra kết nối
if ($conn->connect_error) {
    die("Kết nối không thành công: " . $conn->connect_error);
}

// Kiểm tra nếu form đã submit
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Lấy giá trị từ form và lọc dữ liệu
    $username = trim($_POST["username"]);
    $password = $_POST["password"];

    // Chuẩn bị truy vấn an toàn
    $stmt = $conn->prepare("SELECT password FROM User WHERE username = ?");
    $stmt->bind_param("s", $username);
    $stmt->execute();
    $stmt->store_result();

    // Kiểm tra tài khoản tồn tại
    if ($stmt->num_rows > 0) {
        $stmt->bind_result($hashed_password);
        $stmt->fetch();

        // Kiểm tra mật khẩu
        if (password_verify($password, $hashed_password)) {
            echo "Bạn đã đăng nhập thành công";
            // Chuyển hướng người dùng hoặc khởi tạo session
        } else {
            echo "Sai mật khẩu!";
        }
    } else {
        echo "Tên đăng nhập không tồn tại!";
    }

    $stmt->close();
}

$conn->close();
?>
