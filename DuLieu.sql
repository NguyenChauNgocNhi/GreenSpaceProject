CREATE TABLE Articles (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    content NVARCHAR(MAX) NULL,
    created_at DATETIME2(6) NULL,
    thumbnail_url NVARCHAR(255) NULL,
    title NVARCHAR(255) NULL
);

INSERT INTO Articles (content, created_at, thumbnail_url, title) 
VALUES (N'1. Chọn đất và chậu phù hợp' + CHAR(13) + CHAR(10) +
N'Đất trồng: Ưu tiên đất tơi xốp, thoát nước tốt (pha tro trấu – vỏ thông – xơ dừa), hoặc dùng đất chuyên dụng cho từng nhóm cây (cây thủy sinh, xương rồng, phong lan…).' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +
N'Chậu: Chọn chậu có lỗ thoát nước, kích thước hơi dư so với bộ rễ để cây phát triển.' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +

N'2. Ánh sáng' + CHAR(13) + CHAR(10) +
N'Cây ưa sáng (cây lá xanh sáng, xương rồng, sen đá): đặt ở ban công hoặc gần cửa sổ hướng Đông/Tây.' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +
N'Cây ưa bóng râm (ngọc ngân, trầu bà, lưỡi hổ): để trong phòng gần cửa, tránh nắng gắt.' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +
N'Lưu ý: Tránh ánh nắng trực tiếp buổi trưa gây cháy lá; nếu cần, che màn hoặc di chuyển cây.' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +

N'3. Tưới nước hợp lý' + CHAR(13) + CHAR(10) +
N'Nguyên tắc “khô trên, ẩm dưới”: Đợi bề mặt đất hơi se khô mới tưới tiếp.' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +
N'Tần suất:' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +
N'Cây lá to: 2–3 lần/tuần (mùa hè) – 1 lần/tuần (mùa đông).' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +
N'Xương rồng, sen đá: 1–2 lần/tháng.' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +
N'Cách tưới: Tưới đều lên mặt đất, tránh để nước đọng trên lá (dễ nấm mốc).' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +

N'4. Bón phân và dinh dưỡng' + CHAR(13) + CHAR(10) +
N'Phân bón lá (dạng lỏng): pha loãng, phun trực tiếp lên lá 1–2 lần/tháng.' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +
N'Phân bón gốc (viên nén, granule): rải quanh gốc, 2–3 tháng/lần.' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +
N'Lưu ý: Theo dõi độ tăng trưởng; nếu thấy lá vàng, còi cọc, điều chỉnh lượng phân.' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +

N'5. Điều chỉnh nhiệt độ & độ ẩm' + CHAR(13) + CHAR(10) +
N'Nhiệt độ:' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +
N'Cây nhiệt đới: 18–28°C.' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +
N'Xương rồng: 15–35°C.' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +
N'Độ ẩm: Phun sương cho cây ưa ẩm (phong lan, dương xỉ), giữ độ ẩm quanh 60–80%.' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +

N'6. Cắt tỉa, tạo dáng' + CHAR(13) + CHAR(10) +
N'Loại bỏ lá già, lá vàng, cành khô để kích thích chồi mới.' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +
N'Tỉa ngọn định kỳ giúp cây phân nhánh và cao đều.' + CHAR(13) + CHAR(10) +
N'' + CHAR(13) + CHAR(10) +
N'Dùng kéo sắc, khử trùng trước khi cắt.', '2025-07-04 14:45:36.000000',  N'/images/living-room-8254772_1280.jpg', N'Chăm cây xanh cấp độ 1');

CREATE TABLE Cart_item (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    image_url NVARCHAR(255),
    name NVARCHAR(255),
    price FLOAT,
    product_id BIGINT,
    quantity INT NOT NULL,
    slug NVARCHAR(255),
    username NVARCHAR(255)
);

INSERT INTO Cart_item (image_url, name, price, product_id, quantity, slug, username) 
VALUES (NULL, N'Cây Trầu Bà', 2500000, 52, 1, N'cay-trau-ba', N'ngocnhi123');



CREATE TABLE Feedback (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    created_at DATETIME2(6) NOT NULL,
    email NVARCHAR(255) NOT NULL,
    message NVARCHAR(MAX) NOT NULL,
    name NVARCHAR(255) NOT NULL
);

INSERT INTO Feedback (created_at, email, message, name) 
VALUES	(N'2025-07-04 17:48:31.000000', N'nguyenchaungocnhi04@gmail.com', N'Trang web khá chỉnh chu', N'Nguyễn Châu Ngọc Nhi'),
		(N'2025-07-05 17:36:08.000000', N'loananh@gmail.com', N'Webisite đẹp đấy', N'Loan Anh');



CREATE TABLE Orders (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    address NVARCHAR(255),
    created_at DATETIME2(6),
    email NVARCHAR(255),
    full_name NVARCHAR(255),
    order_code NVARCHAR(255),
    phone NVARCHAR(255),
    shipping_fee FLOAT,
    status NVARCHAR(20),
    total_amount FLOAT,
    username NVARCHAR(255)
);

INSERT INTO Orders (address, created_at, email, full_name, order_code, phone, shipping_fee, status, total_amount, username) 
VALUES	(N'Quận Phú Xuân, TP Huế', '2025-07-04 13:01:26.000000', N'nguyenchaungocnhi04@gmail.com', 'user', 'ORD1746079286713', '0123456789', 30000, 'PROCESSING', 2530000, 'ngocnhi1234'),
		(N'Quận Phú Xuân, TP Huế', '2025-07-04 16:10:41.000000', N'nguyenchaungocnhi04@gmail.com', 'user', 'ORD1746090641327', '0123456789', 30000, 'PROCESSING', 5030000, 'ngocnhi1234');


CREATE TABLE Order_items (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    image_url NVARCHAR(255),
    name NVARCHAR(255),
    price FLOAT,
    product_id BIGINT,
    quantity INT,
    order_id BIGINT FOREIGN KEY REFERENCES orders(id)
);

INSERT INTO Order_items (image_url, name, price, product_id, quantity, order_id) 
VALUES (NULL, N'Cây Trầu Bà', 2500000, 52, 1, 1);


CREATE TABLE Password_reset_otp (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    email NVARCHAR(255),
    expires_at DATETIME2(6),
    otp NVARCHAR(255),
	used_at DATETIME2(6)
);

ALTER TABLE Password_reset_otp
ADD UsedAt DATETIME2;





CREATE TABLE Product (
    product_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    category NVARCHAR(255),
    created_at DATETIME2(6),
    description NVARCHAR(MAX),
    folder_id NVARCHAR(255),
    name NVARCHAR(255) NOT NULL,
    original_price FLOAT,
    price FLOAT NOT NULL,
    slug NVARCHAR(255) NOT NULL UNIQUE,
    stock_quantity INT,
    tags NVARCHAR(255),
    updated_at DATETIME2(6)
);

INSERT INTO Product (product_id, category, created_at, description, folder_id, name, original_price, price, slug, stock_quantity, tags, updated_at) 
VALUES	(1, N'Cây leo, cây cảnh nội thất', '2025-07-06 10:17:08.000000', N'Đẹp, thân thiện với môi trường', NULL, N'Cây thường xuân', 800000, 1000000, 'cay-thuong-xuan', 98, N'Cây cảnh, cây leo', '2025-07-07 17:54:57.000000'),
		(52, N'Cây leo, cây cảnh nội thất', '2025-07-06 11:47:39.000000', N'Cây trầu bà (Epipremnum aureum)là một loại cây cảnh phổ biến được trồng trong nhà hoặc ngoài trời. Cây trầu bà có khả năng sống tốt trong môi trường thiếu ánh sáng và có thể chịu được điều kiện môi trường khắc nghiệt, là một trong những cây dễ chăm sóc nhất', '5555', N'Cây trầu bà', 200000, 2500000, 'cay-trau-ba', 199, N'Cây cảnh, cây leo, cây nội thất', '2025-07-07 17:55:10.000000'),
		(58, N'Cây xanh văn phòng', '2025-07-06 09:15:20.000000', N'Cây lưỡi hổ (Sansevieria) có khả năng hút bụi, thích hợp để bàn làm việc.', '5556', N'Cây lưỡi hổ', 150000, 180000, 'cay-luoi-ho', 120, N'Cây phong thủy, cây văn phòng, cây cảnh', '2025-07-07 17:55:40.000000'),
		(60, N'Cây cảnh, cây bóng mát', '2025-07-06 16:30:45.000000', N'Cây bàng Singapore cao lớn, cho tán rộng, phù hợp trồng lối đi và sân vườn.', '5558', N'Cây bàng Singapore', 800000, 900000, 'cay-bang-singapore', 30, N'Cây leo, cây bóng mát', '2025-07-07 17:55:28.000000'),
		(74, N'Cây cảnh', '2025-07-06 17:27:32.000000', N'Cây khá đẹp, nhỏ mini', NULL, N'Cây sen đá', 1200000, 1000000, 'cay-sen-da', 100, NULL, '2025-07-07 22:33:54.000000'),
		(77, N'Cây cảnh', '2025-07-06 22:34:49.000000', N'Khá đẹp', NULL, N'Cây hạnh phúc', 120000, 100000, 'cay-hanh-phuc', 100, NULL, NULL);


CREATE TABLE Product_image_url (
    product_product_id BIGINT FOREIGN KEY REFERENCES product(product_id) ON DELETE CASCADE ON UPDATE CASCADE,
    image_url NVARCHAR(255)
	CONSTRAINT PK_ProductImage PRIMARY KEY (product_product_id, image_url)
);

INSERT INTO Product_image_url (product_product_id, image_url) 
VALUES	(1, N'images/cay-thuong-xuan.jpg'),
		(52, N'images/cay-trau-ba.jpg'),
		(58, N'images/cay-luoi-ho.jpg'),
		(60, N'images/cay-bang-singapore.jpg'),
		(74, N'images/cay-sen-da-1.jpg'),
		(74, N'images/cay-sen-da-2.jpg'),
		(74, N'images/cay-sen-da-3.jpg'),
		(77, N'images/cay-hanh-phuc.jpg'),
		(77, N'images/cay-hanh-phuc-1.jpg');






CREATE TABLE Tags (
    id BIGINT PRIMARY KEY,
    name NVARCHAR(255) UNIQUE
);

INSERT INTO Tags (id, name) 
VALUES	(8, N'Cây bàng'),
		(1, N'Cây cảnh'),
		(5, N'Cây củ'),
		(2, N'Cây leo'),
		(3, N'Cây thông noel'),
		(7, N'Cây xanh');

CREATE TABLE Product_tags (
    product_id BIGINT,
    tag_id BIGINT,
    PRIMARY KEY (product_id, tag_id),
    FOREIGN KEY (product_id) REFERENCES product(product_id),
    FOREIGN KEY (tag_id) REFERENCES tags(id)
);

INSERT INTO Product_tags (product_id, tag_id)
VALUES	(1, 1),
		(52, 7),
		(58, 1),
		(60, 8),
		(74, 1),
		(77, 1);



CREATE TABLE [User] (
    id INT IDENTITY(1,1) PRIMARY KEY,
    address NVARCHAR(255),
    create_at DATETIME2(6),
    display_name NVARCHAR(255) NOT NULL,
    dob DATE,
    email NVARCHAR(100) NOT NULL,
    password NVARCHAR(255) NOT NULL,
    phone NVARCHAR(255),
    role NVARCHAR(255),
    username NVARCHAR(255) NOT NULL UNIQUE
);

INSERT INTO [User] (address, create_at, display_name, dob, email, password, phone, role, username) 
VALUES	(N'Quận Phú Xuân, TP Huế', '2025-07-07 11:18:28.000000', N'user ngọc nhi', '2024-05-16', 'nguyenchaungocnhi04@gmail.com', 'AQAAAAIAAYagAAAAEL7SADIm0767VeqG207oTGgBc6D7uaUvbteXTBpb3Z9pfy1W7g/oznR5zcANsj1J9g==', '0123456789', N'Admin', 'admin'),
		(N'Bình Định', '2025-07-07 18:24:55.000000', N'Xuân Phong', '2025-05-03', 'xuanphong@gmail.com', '$2a$10$2bE4HoVxjS/xFgq6fkg5S.8chC7nqovudYf3pkfEKX5iS7V4ejQSe', '0132456789', N'Khách hàng', 'xuanphong');





ALTER TABLE Articles ADD CONSTRAINT PK_Articles PRIMARY KEY (id);
ALTER TABLE Cart_item ADD CONSTRAINT PK_Cart_item PRIMARY KEY (id);
ALTER TABLE Feedback ADD CONSTRAINT PK_Feedback PRIMARY KEY (id);
ALTER TABLE Orders ADD CONSTRAINT PK_Orders PRIMARY KEY (id);
ALTER TABLE Order_items ADD CONSTRAINT PK_Order_items PRIMARY KEY (id);
ALTER TABLE Password_reset_otp ADD CONSTRAINT PK_Password_reset_otp PRIMARY KEY (id);
ALTER TABLE Product ADD CONSTRAINT PK_Product PRIMARY KEY (product_id);
ALTER TABLE Product_tags ADD CONSTRAINT PK_Product_tags PRIMARY KEY (product_id, tag_id);
ALTER TABLE Tags ADD CONSTRAINT PK_Tags PRIMARY KEY (id);
ALTER TABLE [User] ADD CONSTRAINT PK_User PRIMARY KEY (id);

ALTER TABLE Product ADD CONSTRAINT UQ_Product_Slug UNIQUE (slug);
ALTER TABLE Tags ADD CONSTRAINT UQ_Tags_Name UNIQUE (name);
ALTER TABLE [User] ADD CONSTRAINT UQ_User_Username UNIQUE (username);

ALTER TABLE Order_items 
    ADD CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (order_id) REFERENCES Orders(id);

ALTER TABLE Product_image_url 
    ADD CONSTRAINT FK_ProductImage_Product FOREIGN KEY (product_product_id) 
    REFERENCES Product(product_id) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE Product_tags 
    ADD CONSTRAINT FK_ProductTags_Product FOREIGN KEY (product_id) REFERENCES Product(product_id);

ALTER TABLE Product_tags 
    ADD CONSTRAINT FK_ProductTags_Tags FOREIGN KEY (tag_id) REFERENCES Tags(id);

