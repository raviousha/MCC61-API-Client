var data = document.getElementById("judul");
$(function () {
    $(data).click(
        function () {
            let name = prompt("Please enter your name:", "John Doe");
            if (name == null || name == "") {
                $('.display-excercise').css({ "background-color": "#000000", "transition": "background-color 0.5s ease" });
                text = "It's a pity that you dont tell me your name :(";
            } else {
                $('.display-excercise').css({ "background-color": "#31072e", "color": "#4ECD7D", "transition": "background-color 0.5s ease" });
                text = "Hello " + name + "! How are you today?";
            }
            data.innerHTML = text;

            setTimeout(function () {
                $('.display-excercise').css({ "background-color": "#013030", "color": "#519259", "transition": "background-color 0.5s ease" });
                data.innerHTML = "MCC";
            }, 3500);
        });
});

$(function changebg() {
    $('#btn-bg').click(
        function () {
            $('body').css({ "background-color": "#036666", "transition": "background-color 0.5s ease" });
            $('.contents').css({ "color": "#77D99B", "transition": "color 0.5s ease" });
            $('#btn-bg').html("Double click to return to Dark");
        });

    $('#btn-bg').dblclick(
        function () {
            $('body').css({ "background-color": "#434247", "transition": "background-color 0.5s ease" });
            $('.contents').css({ "color": "ivory", "transition": "color 0.5s ease" });
            $('#btn-bg').html("Change Background to Light");
        });
});

var ids = "";
var diffImg = function (ids) {
    $('.image').click(
        function () {
            if (ids == "orange") {
                $('#img-main').attr('src', 'https://www.dahon.jp/2021/product/bike/img/calm/img01.jpg');
            }
            else if (ids == "stone") {
                $('#img-main').attr('src', 'https://www.dahon.jp/2021/product/bike/img/calm/img02.jpg');
            }
            else if (ids == "blue") {
                $('#img-main').attr('src', 'https://www.dahon.jp/2021/product/bike/img/calm/img03.jpg');
            }
        });
    setTimeout(function () {
        $('#img-main').attr('src', 'https://www.dahon.jp/2021/product/bike/img/calm/img10.jpg');
    }, 3500);
}

$(function close() {
    $('.fixed').dblclick(
        function () {
            $('.fixed').hide();
        });
});

const animals = [
    { name: 'bimo', species: 'cat', kelas: { name: "mamalia" } },
    { name: 'budi', species: 'cat', kelas: { name: "mamalia" } },
    { name: 'nemo', species: 'snail', kelas: { name: "invertebrata" } },
    { name: 'dori', species: 'cat', kelas: { name: "mamalia" } },
    { name: 'simba', species: 'snail', kelas: { name: "invertebrata" } }
]

console.log(animals);

$(function loopingCat() {
    let onlyCat = [];
    for (var i = 0; i < animals.length; i++) {
        if (animals[i].species == "cat") {
            onlyCat.push(animals[i]);
        }
    }
    console.log(onlyCat);
});

$(function loopingClass() {
    for (var i = 0; i < animals.length; i++) {
        if (animals[i].species == "snail") {
            animals[i].kelas.name = "Non-Mamalia";
        }
    }
    console.log(animals);
});