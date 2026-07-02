# Prototype 4

Một bản prototype cơ chế gameplay đấu trường 3D (sumo rolling arena) hoàn thành trong lộ trình Junior Programmer Pathway của Unity.

Dự án này giới thiệu các cơ chế đẩy lùi vật lý, các đợt kẻ địch (waves) tăng dần độ khó, hệ thống nhặt vật phẩm tăng sức mạnh (powerups) và được cải tiến mạnh mẽ bằng kiến trúc ScriptableObjects để tách biệt hoàn toàn logic của hệ thống powerups khỏi controller của người chơi.

## Demo Gameplay
![Demo Gameplay](Screenshots/DemoGameplay.gif)

## Những Điều Tôi Đã Học Được

### [Lesson 4.1 - Kick Back](https://learn.unity.com/pathway/junior-programmer/unit/gameplay-mechanics/tutorial/lesson-4-1-kick-back?version=6.0)

**Tính năng mới**

- Khởi tạo dự án đấu trường hình tròn, thiết lập camera xoay theo hướng nhìn và nhập các mô hình starter.
- Điều khiển quả cầu người chơi lăn theo hướng nhìn của camera dựa trên phím bấm dọc (Vertical Input).
- Thiết lập kẻ địch tự động tìm và di chuyển hướng về phía người chơi.

**Khái niệm & kỹ năng mới**

- **Lực di chuyển theo hướng nhìn (Look Direction):** Sử dụng các vector hướng trước của camera (`camera.transform.forward`) để áp dụng lực đẩy di chuyển chính xác lên quả cầu người chơi.
- **Vật lý tìm đường cơ bản:** Tính toán vector hướng di chuyển của kẻ địch bằng cách trừ vị trí của người chơi cho vị trí kẻ địch và chuẩn hóa (`.normalized`).

### [Lesson 4.2 - Power Up](https://learn.unity.com/pathway/junior-programmer/unit/gameplay-mechanics/tutorial/4-2-power-up?version=6.0)

**Tính năng mới**

- Đặt các vật phẩm Powerup trong màn chơi và kích hoạt vòng tròn chỉ báo (indicator ring) xoay quanh người chơi khi nhặt được.
- Thiết lập lực va chạm vật lý cực lớn khi có trạng thái Powerup hoạt động để đẩy bay kẻ địch ra ngoài rìa đấu trường.
- Tự động tắt trạng thái Powerup sau một khoảng thời gian chờ (cooldown).

**Khái niệm & kỹ năng mới**

- **Nối vị trí chỉ báo (Indicator Follow):** Cập nhật vị trí của vòng tròn hiệu ứng theo vị trí của người chơi trong mỗi khung hình.
- **Coroutines đếm ngược:** Sử dụng `IEnumerator` và `yield return new WaitForSeconds` để xử lý thời gian duy trì hiệu ứng nâng cấp sức mạnh.
- **Độ lớn lực va chạm (Collision Impulse):** Tính toán hướng đẩy dội ngược của kẻ địch dựa trên vị trí va chạm cục bộ và nhân với một hệ số lực xung kích.

### [Lesson 4.3 - Spawn Waves](https://learn.unity.com/pathway/junior-programmer/unit/gameplay-mechanics/tutorial/4-3-spawn-waves?version=6.0)

**Tính năng mới**

- Xây dựng một Spawn Manager tự động tính toán vị trí ngẫu nhiên trên bề mặt đấu trường để sinh ra kẻ địch và các vật phẩm Powerup.
- Thiết lập hệ thống đợt đấu (waves): sau khi tiêu diệt hết kẻ địch hiện tại, đợt kẻ địch mới sẽ sinh ra với số lượng tăng thêm 1 con.

**Khái niệm & kỹ năng mới**

- **Hệ thống vòng lặp Wave logic:** Đếm số lượng kẻ địch đang hoạt động trong scene bằng cách sử dụng `FindObjectsOfType`.
- **Tách biệt hàm:** Viết các hàm sinh vật thể có tham số truyền vào để tái sử dụng cho nhiều số lượng kẻ địch khác nhau.

## Các Tính Năng Mở Rộng & Cải Tiến (Extras)

- **Tái cấu trúc mã nguồn sạch:** Tổ chức lại mã nguồn từ bài học mẫu, tối ưu hóa các lớp đơn nhiệm để loại bỏ sự phụ thuộc chéo lộn xộn.
- **Hệ thống Powerup dựa trên ScriptableObject:** Khắc phục hoàn toàn lỗi thiết kế của bài học gốc (nơi toàn bộ logic của các loại powerups khác nhau bị viết cứng bên trong script của người chơi, biến nó thành một God Class khó bảo trì và mở rộng). Hệ thống mới chia tách logic một cách linh hoạt:
  - **`BasePowerupData`**: Lớp trừu tượng (abstract ScriptableObject) cơ sở cho tất cả các loại powerup. Nó định nghĩa dữ liệu cấu hình và chứa các phương thức xử lý trong suốt vòng đời hoạt động của powerup đó.
  - **`IPowerupUser`**: Interface bắt buộc cho bất kỳ đối tượng nào (người chơi hoặc kẻ địch) muốn sử dụng các loại powerup, đảm bảo tính đa hình và giao tiếp an toàn.
  - **`PowerupInstance`**: Lớp bao bọc trung gian (wrapper class) chịu trách nhiệm kết nối giữa loại powerup và đối tượng sử dụng. Nó giúp loại bỏ sự liên kết chặt chẽ (tight coupling) bằng cách che giấu các chi tiết cụ thể của từng loại powerup; đối tượng sử dụng chỉ cần gọi wrapper để thực hiện hành vi.
  - **`PowerupItem`**: Thành phần đặt trên các GameObject nhặt được trong màn chơi. Khi người chơi chạm vào, class này sẽ khởi tạo một `PowerupInstance` tương ứng và chuyển nó tới người chơi sử dụng.
