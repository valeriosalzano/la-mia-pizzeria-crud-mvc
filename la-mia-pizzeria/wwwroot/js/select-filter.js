const selectFilter = document.querySelector("#ingredients-filter-input");

const selectOptions = document.querySelectorAll("#SelectedIngredientsId > option");

if (selectFilter) {
    selectFilter.addEventListener("keyup", () => {
        let filterValue = selectFilter.value;
        selectOptions.forEach(option => {
            console.log(option);
            if (!option.innerHTML.match(new RegExp(`${filterValue}`, 'i'))) {
                option.classList.add("d-none");
            } else {
                option.classList.remove("d-none");
            }
        })
    } )
}