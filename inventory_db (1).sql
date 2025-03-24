-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 24, 2025 at 09:52 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `inventory_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `ProductID` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `Supplier` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`ProductID`, `Name`, `Quantity`, `Price`, `Supplier`) VALUES
(1, 'Laptop', 125, 800.00, 'Tech Solutions'),
(2, 'Smartphone	', 25, 500.00, 'Mobile Hub'),
(3, 'Keyboard', 30, 40.00, 'Peripherals Inc.'),
(4, 'Mouse', 268, 20.00, 'Peripherals Inc.'),
(5, 'Monitor', 15, 150.00, 'Display World');

-- --------------------------------------------------------

--
-- Table structure for table `transactions`
--

CREATE TABLE `transactions` (
  `TransactionID` int(11) NOT NULL,
  `ProductID` int(11) NOT NULL,
  `Transaction_Type` varchar(50) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `Date` datetime NOT NULL,
  `ProductName` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `transactions`
--

INSERT INTO `transactions` (`TransactionID`, `ProductID`, `Transaction_Type`, `Quantity`, `Date`, `ProductName`) VALUES
(3, 1, 'IN', 50, '2025-03-18 02:06:04', 'Laptop'),
(4, 4, 'OUT', 12, '2025-03-18 02:28:55', 'Mouse'),
(13, 4, 'OUT', 2, '2025-03-22 07:19:12', 'Mouse'),
(14, 1, 'OUT', 5, '2025-03-22 07:23:15', 'Laptop'),
(15, 1, 'IN', 65, '2025-03-22 07:23:21', 'Laptop'),
(16, 4, 'IN', 232, '2025-03-25 03:00:00', 'Mouse'),
(17, 5, 'IN', 10, '2025-03-25 03:47:53', 'Monitor'),
(18, 5, 'OUT', 15, '2025-03-25 03:48:27', 'Monitor');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`ProductID`);

--
-- Indexes for table `transactions`
--
ALTER TABLE `transactions`
  ADD PRIMARY KEY (`TransactionID`),
  ADD KEY `ProductID` (`ProductID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `ProductID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `transactions`
--
ALTER TABLE `transactions`
  MODIFY `TransactionID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `transactions`
--
ALTER TABLE `transactions`
  ADD CONSTRAINT `transactions_ibfk_1` FOREIGN KEY (`ProductID`) REFERENCES `products` (`ProductID`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
