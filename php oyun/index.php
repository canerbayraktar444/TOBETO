<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sayı Bulma Oyunu</title>
    <link rel="stylesheet" href="styles.css">
</head>
<body>
    <h1>Sayı Bulma Oyununa Hoş Geldiniz</h1>
    <button onclick="startGame()">Oyuna BAŞLA</button>
    <div id="formContainer" style="display: none;">
        <form action="game.php" method="post">
            <label for="name">Ad:</label>
            <input type="text" id="name" name="name" required><br><br>
            <label for="surname">Soyad:</label>
            <input type="text" id="surname" name="surname" required><br><br>
            <label for="email">Eposta:</label>
            <input type="email" id="email" name="email" required><br><br>
            <input type="submit" value="Devam">
        </form>
    </div>
</body>
</html>
