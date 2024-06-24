let sales = [];
let products = [];
let totalPriceSale = 0;
function closeErrorModal() {
    $('#errorModal').modal('hide');
}

function closeModalEmail() {
    $('#errorModalEmail').modal('hide');
}

function closeProductsModal() {
    $('#errorModalProd').modal('hide');
}

function validateEmail(email) {
    const re = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    return re.test(String(email).toLowerCase());
}

function SetData() {

    const nameForm = document.getElementById('name').value;
    const descriptionForm = document.getElementById('description').value;
    const mailForm = document.getElementById('mail').value;
    const totalForm = parseFloat(document.getElementById('total').value);

    if (nameForm !== "" && descriptionForm !== "" && mailForm !== "" && totalForm !== "") {

        if (validateEmail(mailForm)) {

            let listidProds = [];

            sales.forEach(sale => {

                for (let i = 0; i < sale.quantity; i++) {

                    const prdId = {

                        idProduct: sale.idProduct

                    }

                    listidProds.push(prdId);

                }


            })

            if (listidProds.length === 0) {
                $('#errorModalProd').modal('show');
            }
            else {
                console.log(listidProds);

                const data = {
                    nameClient: nameForm,
                    description: descriptionForm,
                    mail: mailForm,
                    totalPrice: totalForm,
                    salesProducts: listidProds

                };

                fetch('/Sale/createSale', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(data)
                })
                    .then(response => response.json())
                    .then(data => {
                        console.log('Success:', data);
                        location.reload();
                    })
                    .catch((error) => {
                        console.error('Error:', error);
                        alert('There was an error creating the product.');
                    });
            }

            
        }
        else {
            $('#errorModalEmail').modal('show');
        }
        
    }else {

        $('#errorModal').modal('show');

    }

}
function RestQuantity(idProduct) {

    products.forEach((item, index) => {

        if (item.idProduct == idProduct) {

            if (products[index].quantity > 0) {
                products[index].quantity -= 1;
            }

        }

    })

    CreateTable(products)

}
function RestQuantitySale(idProduct) {

    products.forEach((item, index) => {

        if (item.idProduct == idProduct) {

            products[index].quantity += 1;

        }

    })

    CreateTable(products)
    LessProduct(idProduct)

}
function LessProduct(idProduct) {

    const productExistence = sales.find(sale => sale.idProduct === idProduct);

    if (productExistence ) {

        productExistence.quantity -= 1;
        productExistence.totalPrice = productExistence.quantity * productExistence.unitPrice;
        totalPriceSale -= productExistence.totalPrice;

    }

    updateTableSales();

}
function AddProduct(idProduct, productName, unitPrice) {

    const product = products.find(p => p.idProduct === idProduct);

    if (product) {

        if (product.quantity > 0) {

            const productExistence = sales.find(sale => sale.idProduct === idProduct);

            if (productExistence) {

                productExistence.quantity += 1;
                productExistence.totalPrice = productExistence.quantity * unitPrice;
                totalPriceSale = productExistence.totalPrice;

            } else {

                const newSale = {
                    idProduct,
                    productName,
                    quantity: 1,
                    unitPrice,
                    totalPrice: unitPrice
                };
                sales.push(newSale);
            }

        }

    }

    updateTableSales();

}
function CreateTable(data) {

    const tbody = document.querySelector('#tableProduct');
    tbody.innerHTML = ''; // Limpiar filas existentes

    data.forEach(dato => {
            const unitPriceFormatted = parseFloat(dato.unitPrice).toFixed(2);
            const row = `<tr>
                                            <td>${dato.productName}</td>
                                            <td>${dato.quantity}</td>
                                            <td>${unitPriceFormatted} $</td>
                                            <td>
                                            <button type="button" class="btnAdd" onclick="AddProduct(${dato.idProduct},'${dato.productName}','${dato.unitPrice}'); RestQuantity(${dato.idProduct}) " ><i class="bi bi-plus-circle"></i></button>
                                            </td>
                                         </tr>`;
            tbody.innerHTML += row; 
    });
    
}
function updateTableSales() {

    const salesBody = document.querySelector('#saleBody');
    salesBody.innerHTML = '';

    sales.forEach(sale => {

        if (sale.quantity > 0) {

            let totalPrice = parseFloat(sale.unitPrice).toFixed(2);

            const row = `
                    <tr>
                    <td>${sale.productName}</td>
                    <td>${sale.quantity}</td>
                    <td>${totalPrice} $</td>
                    <td><button type="button" class="btnLess" onclick="RestQuantitySale(${sale.idProduct})" ><i class="bi bi-dash-circle"></i></button></td>
                   </tr>`;
            salesBody.innerHTML += row;

        }
       
    });

    let allProducts = sales.flat();
    let totalPriceSale = allProducts.reduce((total, product) => total + parseFloat(product.totalPrice || 0), 0);
    document.getElementById('total').value = totalPriceSale.toFixed(2);
}
function Load_Data() {
        fetch('/Sale/Products_Data', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Error en la solicitud');
            }
            return response.json();
        })
        .then(data => {

            data.forEach(item => {

                if (item.active != false) {

                    let product = {
                        idProduct: item.idProduct,
                        productName: item.productName,
                        quantity: item.quantity,
                        unitPrice: item.unitPrice
                    };

                    products.push(product);
                }

            });
           
            CreateTable(products);
        })

        .catch(error => {
            console.error('Error:', error);
        });
}

document.addEventListener('DOMContentLoaded', function () {

    Load_Data()

});
