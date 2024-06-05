<?php
session_start();

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $_SESSION['name'] = $_POST['name'];
    $_SESSION['surname'] = $_POST['surname'];
    $_SESSION['email'] = $_POST['email'];

    // Initialize game
    $_SESSION['secretNumber'] = rand(1, 1000);
    $_SESSION['chances'] = 10;
    $_SESSION['guesses'] = [];
}

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sayı Bulma Oyunu</title>
    <link rel="stylesheet" href="styles.css">
</head>
<body>
    <div id="gameContainer">
        <h2>Tahmin Edin (1-1000 arası)</h2>
        <form action="game.php" method="post">
            <input type="number" name="guess" required>
            <input type="submit" value="Tahmin Et">
        </form>
        <p id="hint">
            <?php
            if ($_SERVER["REQUEST_METHOD"] == "POST") {
                $guess = $_POST['guess'];
                array_push($_SESSION['guesses'], $guess);
                $_SESSION['chances']--;

                if ($guess == $_SESSION['secretNumber']) {
                    echo "Tebrikler! Sayıyı buldunuz.";
                } elseif ($_SESSION['chances'] == 0) {
                    echo "Üzgünüm! Hakkınız doldu. Doğru cevap: " . $_SESSION['secretNumber'];
                } elseif ($guess < $_SESSION['secretNumber']) {
                    echo "Daha büyük bir sayı girin.";
                } else {
                    echo "Daha küçük bir sayı girin.";
                }
            }
            ?>
        </p>
        <p id="chances">Kalan Hakkınız: <?php echo $_SESSION['chances']; ?></p>
    </div>
</body>
</html>
