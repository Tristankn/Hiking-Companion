async function getGearList() {
    const response = await fetch(
         "http://localhost:5289/gear/"
    );

    if(!response.ok){
        throw new Error("Could not get gear list")
    }

    const allGear = await response.json();

    const gearList = document.getElementById("gearList");

    allGear.forEach(gear => {
        console.log(gear);
        const li = document.createElement("li");
        li.textContent = gear.name;
        gearList.appendChild(li);
        
    });

    
}

getGearList();