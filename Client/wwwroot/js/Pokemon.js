$.ajax({
    url: 'https://pokeapi.co/api/v2/pokemon/?limit=100'
}).done((data) => {
    var text = ""
    $.each(data.results, function (key, pokemon) {
        text += `<tr>
                    <td>${key += 1}</td>
                    <td>${pokemon.name}</td>
                    <td ">
                    <button href="#${pokemon.name}" class="btn btn-danger fas fa-info" data-toggle="modal" data-target="#pokeModal" onclick='detail("${pokemon.url}")'></button>
                    </td>
                 </tr>`
    })
    $("#pokeTable").html(text)
}).fail((error) => {
    console.log(error)
})

function detail(url) {
    $.ajax({
        url: url
    }).done((pokemon) => {
        var title = ""
        var image = ""
        var badgeType = ""
        var stat = ""
        var moves = ""
        var text = ""
        var ability = ""
        title += `<h2 class="display-4 align-middle">${pokemon.name}</h2>`
        image += `<img class="img-pokemon" src="${pokemon.sprites.other.dream_world.front_default}" alt="${pokemon.name}"><br>`
        $.each(pokemon.types, function (count) {
            tipe = pokemon.types[count].type.name
            if (tipe == 'grass' || tipe == 'bug') {
                badgeType += `
                    <p class="badge badge-success">${tipe}</p>
                `
            }
            else if (tipe == 'water' || tipe == 'ice') {
                badgeType += `
                    <p class="badge badge-primary">${tipe}</p>
                `
            }
            else if (tipe == 'rock' || tipe == 'steel') {
                badgeType += `
                    <p class="badge badge-secondary">${tipe}</p>
                `
            }
            else if (tipe == 'ground' || tipe == 'fighting') {
                badgeType += `
                    <p class="badge badge-dark" style="background-color: #876445">${tipe}</p>
                `
            }
            else if (tipe == 'fire' || tipe == 'dragon') {
                badgeType += `
                    <p class="badge badge-danger">${tipe}</p>
                `
            }
            else if (tipe == 'electric') {
                badgeType += `
                    <p class="badge badge-warning">${tipe}</p>
                `
            }
            else if (tipe == 'fairy') {
                badgeType += `
                    <p class="badge badge-light" style="background-color: lightyellow">${tipe}</p>
                `
            }
            else if (tipe == 'poison' || tipe == 'psychic') {
                badgeType += `
                    <p class="badge badge-dark" style="background-color: purple">${tipe}</p>
                `
            }
            else if (tipe == 'dark' || tipe == 'ghost') {
                badgeType += `
                    <p class="badge badge-dark" style="background-color: navy">${tipe}</p>
                `
            }
            else {
                badgeType += `
                    <p class="badge badge-light">${tipe}</p>
                `
            }
        })
        text += `<tr>
                    <td>Height</td>
                    <td>${pokemon.height}</td>
                 </tr>
                 <tr>
                    <td>Weight</td>
                    <td>${pokemon.weight}</td>
                 </tr>`
        $.each(pokemon.abilities, function (count) {
            abi = pokemon.abilities[count].ability.name
            ability += `<tr>
                            <td>${abi}</td>
                        </tr>`
        })

        $.each(pokemon.stats, function (count) {
            stname = pokemon.stats[count].stat.name
            st = pokemon.stats[count].base_stat
            stat += `<tr>
                        <td>${stname}</td>
                        <td>${st}</td>
                    </tr>`
        })

        $.each(pokemon.moves, function (count) {
            mv = pokemon.moves[count].move.name
            moves += `<tr>
                        <td>${mv}</td>
                      </tr>`
            return count < 4;
        })

        $("#column-title").html(title)
        $("#column-2").html(image + "<br>" + badgeType)
        $("#infoTable").html(text)
        $("#statTable").html(stat)
        $("#abilityTable").html(ability)
        $("#moveTable").html(moves)
    }).fail((error) => {
        console.log(error)
    })
}