-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 06 Agu 2020 pada 08.04
-- Versi server: 10.4.13-MariaDB
-- Versi PHP: 7.4.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `kalkulatorutami`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `kalkulator`
--

CREATE TABLE `kalkulator` (
  `id` int(255) NOT NULL,
  `Ekspresi` varchar(255) NOT NULL,
  `Preorder` varchar(1000) NOT NULL,
  `Postorder` varchar(1000) NOT NULL,
  `Desimal` int(255) NOT NULL,
  `Binari` int(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `kalkulator`
--

INSERT INTO `kalkulator` (`id`, `Ekspresi`, `Preorder`, `Postorder`, `Desimal`, `Binari`) VALUES
(37, '5-8', '5 8 -', '- 8 5 ', -3, 2147483647),
(43, '5+', '5  +', '+  5 ', 5, 101);

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `kalkulator`
--
ALTER TABLE `kalkulator`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `Ekspresi` (`Ekspresi`),
  ADD UNIQUE KEY `id` (`id`);

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `kalkulator`
--
ALTER TABLE `kalkulator`
  MODIFY `id` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=44;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
