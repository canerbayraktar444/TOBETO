<?php
// Oyun tahtasını başlatma
function initializeBoard($boardSize) {
    return array_fill(0, $boardSize, array_fill(0, $boardSize, ' '));
}

// Gemi yerleştirme
function placeShips(&$board, $ships) {
    $size = count($board);
    for ($i = 0; $i < $ships; $i++) {
        do {
            $x = rand(0, $size - 1);
            $y = rand(0, $size - 1);
        } while ($board[$x][$y] == 'S');
        $board[$x][$y] = 'S';
    }
}

// Tahtayı yazdırma
function printBoard($board) {
    $size = count($board);
    echo "  ";
    for ($i = 0; $i < $size; $i++) {
        echo "$i ";
    }
    echo "\n";
    for ($i = 0; $i < $size; $i++) {
        echo "$i ";
        for ($j = 0; $j < $size; $j++) {
            echo $board[$i][$j] . ' ';
        }
        echo "\n";
    }
}

// Atış yapma
function shoot(&$board, $x, $y) {
    if ($board[$x][$y] == 'S') {
        $board[$x][$y] = 'X';
        return true;
    } elseif ($board[$x][$y] == ' ') {
        $board[$x][$y] = 'O';
        return false;
    }
    return false;
}

// Oyun döngüsü
function playGame($boardSize, $ships) {
    $playerBoard = initializeBoard($boardSize);
    $computerBoard = initializeBoard($boardSize);
    placeShips($playerBoard, $ships);
    placeShips($computerBoard, $ships);

    $playerHits = 0;
    $computerHits = 0;
    while ($playerHits < $ships && $computerHits < $ships) {
        echo "Bilgisayarın tahtası:\n";
        printBoard($computerBoard);

        // Oyuncu hamlesi
        do {
            echo "X koordinatını girin: ";
            $x = trim(fgets(STDIN));
            echo "Y koordinatını girin: ";
            $y = trim(fgets(STDIN));
        } while ($x < 0 || $x >= $boardSize || $y < 0 || $y >= $boardSize);

        if (shoot($computerBoard, $x, $y)) {
            echo "Vurdunuz!\n";
            $playerHits++;
        } else {
            echo "Iskaladınız.\n";
        }

        // Bilgisayar hamlesi
        do {
            $x = rand(0, $boardSize - 1);
            $y = rand(0, $boardSize - 1);
        } while ($playerBoard[$x][$y] == 'X' || $playerBoard[$x][$y] == 'O');

        if (shoot($playerBoard, $x, $y)) {
            echo "Bilgisayar vurdu!\n";
            $computerHits++;
        } else {
            echo "Bilgisayar ıskaladı.\n";
        }

        echo "Sizin tahtanız:\n";
        printBoard($playerBoard);
    }

    if ($playerHits == $ships) {
        echo "Tebrikler! Tüm gemileri batırdınız.\n";
    } else {
        echo "Üzgünüm, bilgisayar tüm gemilerinizi batırdı.\n";
    }
}

// Oyun başlatma
$boardSize = 5;
$ships = 3;
playGame($boardSize, $ships);
?>
