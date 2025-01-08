-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 07 Jan 2025 pada 15.19
-- Versi server: 10.4.28-MariaDB
-- Versi PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `cashierdb`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `inventory_log`
--

CREATE TABLE `inventory_log` (
  `log_id` int(11) NOT NULL,
  `change_id` int(11) NOT NULL,
  `change_quantity` int(11) NOT NULL,
  `mode` varchar(15) NOT NULL,
  `datetime` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktur dari tabel `member`
--

CREATE TABLE `member` (
  `member_id` int(11) NOT NULL,
  `nik` varchar(20) NOT NULL,
  `name` varchar(100) NOT NULL,
  `address` varchar(100) NOT NULL,
  `shopping` int(11) NOT NULL,
  `created_at` date NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `member`
--

INSERT INTO `member` (`member_id`, `nik`, `name`, `address`, `shopping`, `created_at`) VALUES
(1, '330809123123', 'zakinonstop', 'Magelang', 3, '2025-01-07'),
(3, '1234', 'shabbah', 'Sleman', 1, '2025-01-07');

-- --------------------------------------------------------

--
-- Struktur dari tabel `product`
--

CREATE TABLE `product` (
  `product_id` int(11) NOT NULL,
  `code` varchar(100) NOT NULL,
  `name` varchar(100) NOT NULL,
  `stock` int(11) NOT NULL,
  `price` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `product`
--

INSERT INTO `product` (`product_id`, `code`, `name`, `stock`, `price`) VALUES
(4, '111', 'roti coy', 12, 3500),
(8, '121', 'aoka', 10, 3000);

-- --------------------------------------------------------

--
-- Struktur dari tabel `restock`
--

CREATE TABLE `restock` (
  `restock_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `product_id` int(11) NOT NULL,
  `change_quantity` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktur dari tabel `role`
--

CREATE TABLE `role` (
  `role_id` int(2) NOT NULL,
  `name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `role`
--

INSERT INTO `role` (`role_id`, `name`) VALUES
(1, 'kasir'),
(2, 'admin');

-- --------------------------------------------------------

--
-- Struktur dari tabel `transaction`
--

CREATE TABLE `transaction` (
  `transaction_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `transaction_detail_id` int(11) NOT NULL,
  `datetime` datetime NOT NULL,
  `total_amount` int(11) NOT NULL,
  `paid` float NOT NULL,
  `changed` float NOT NULL,
  `member_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `transaction`
--

INSERT INTO `transaction` (`transaction_id`, `user_id`, `transaction_detail_id`, `datetime`, `total_amount`, `paid`, `changed`, `member_id`) VALUES
(34, 1, 1, '2025-01-07 17:36:22', 7840, 0, 0, 0),
(35, 1, 2, '2025-01-07 17:37:43', 21840, 0, 0, 0),
(36, 1, 3, '2025-01-07 17:38:01', 11760, 0, 0, 0),
(38, 1, 2, '2025-02-06 13:40:12', 10000, 20000, 10000, 2);

-- --------------------------------------------------------

--
-- Struktur dari tabel `transaction_detail`
--

CREATE TABLE `transaction_detail` (
  `urut` int(11) NOT NULL,
  `transaction_detail_id` int(11) NOT NULL,
  `product_id` int(11) NOT NULL,
  `qty` int(11) NOT NULL,
  `price` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `transaction_detail`
--

INSERT INTO `transaction_detail` (`urut`, `transaction_detail_id`, `product_id`, `qty`, `price`) VALUES
(189, 1, 4, 1, 3500),
(190, 2, 4, 1, 3500),
(191, 2, 8, 1, 3000),
(192, 3, 4, 1, 3500),
(194, 5, 4, 1, 3500),
(195, 6, 8, 1, 3000),
(196, 1, 4, 1, 3500),
(197, 2, 8, 1, 3000),
(198, 2, 4, 1, 3500),
(199, 2, 8, 1, 3000),
(200, 2, 4, 1, 3500),
(201, 3, 4, 1, 3500),
(202, 3, 4, 1, 3500),
(205, 4, 4, 1, 3500),
(206, 4, 4, 1, 3500),
(207, 4, 4, 1, 3500),
(208, 4, 8, 1, 3000),
(209, 4, 4, 1, 3500);

-- --------------------------------------------------------

--
-- Struktur dari tabel `user`
--

CREATE TABLE `user` (
  `user_id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(20) NOT NULL,
  `role_id` int(2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `user`
--

INSERT INTO `user` (`user_id`, `username`, `password`, `role_id`) VALUES
(1, 'kasir', 'kasir', 1),
(2, 'admin', 'admin', 2),
(3, 'zakikasir', 'zakikasir', 1);

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `inventory_log`
--
ALTER TABLE `inventory_log`
  ADD PRIMARY KEY (`log_id`);

--
-- Indeks untuk tabel `member`
--
ALTER TABLE `member`
  ADD PRIMARY KEY (`member_id`);

--
-- Indeks untuk tabel `product`
--
ALTER TABLE `product`
  ADD PRIMARY KEY (`product_id`);

--
-- Indeks untuk tabel `role`
--
ALTER TABLE `role`
  ADD PRIMARY KEY (`role_id`);

--
-- Indeks untuk tabel `transaction`
--
ALTER TABLE `transaction`
  ADD PRIMARY KEY (`transaction_id`);

--
-- Indeks untuk tabel `transaction_detail`
--
ALTER TABLE `transaction_detail`
  ADD PRIMARY KEY (`urut`);

--
-- Indeks untuk tabel `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`user_id`);

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `inventory_log`
--
ALTER TABLE `inventory_log`
  MODIFY `log_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT untuk tabel `member`
--
ALTER TABLE `member`
  MODIFY `member_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT untuk tabel `product`
--
ALTER TABLE `product`
  MODIFY `product_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT untuk tabel `role`
--
ALTER TABLE `role`
  MODIFY `role_id` int(2) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT untuk tabel `transaction`
--
ALTER TABLE `transaction`
  MODIFY `transaction_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;

--
-- AUTO_INCREMENT untuk tabel `transaction_detail`
--
ALTER TABLE `transaction_detail`
  MODIFY `urut` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=210;

--
-- AUTO_INCREMENT untuk tabel `user`
--
ALTER TABLE `user`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
