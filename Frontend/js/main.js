import { CreateTable, TableToContainer } from "./dynamicTables.js";
import { FormElements, CreateFormCont, CheckData } from "./dynamicForms.js";
import { CreateMsgAlert } from "./msgAlert.js";
import { CreateUrl, SendReguest } from "./crud.js";

document.addEventListener("DOMContentLoaded", () => {
  const btnsNav = document.querySelectorAll(".btn_nav");
  const mainContainer = document.querySelector("#mainContent");
  const mainPageTitle = document.querySelector("#mainTitle");

  const LoadActionBtns = (url, title) => {
    console.log(url);
    const btnDelete = document.querySelectorAll("#deleteBtn");
    const btnUpdate = document.querySelectorAll("#editBtn");

    btnDelete.forEach((btn) => {
      btn.addEventListener("click", (e) => {
        e.preventDefault();
        const idCel = e.target
          .closest("tr")
          .querySelector(".idCel").textContent;
        console.log(idCel);
        let urlEnti = e.target.getAttribute("name");
        if (urlEnti == "Clientes") {
          urlEnti = "Client";
        }
        if (urlEnti == "Productos") {
          urlEnti = "Product";
        }
        if (urlEnti == "Facturas") {
          urlEnti = "Invoice";
        }
        if (urlEnti == "Detalle de Facturas") {
          urlEnti = "InvoiceDetail";
        }
        let urlId = idCel;
        const urlReq = `http://localhost:5226/api/${urlEnti}/${urlId}`;
        SendReguest(urlReq, "delete", "").then((data) => {
          return data.text();
        }).then(msg => {
          console.log(msg);
          CreateDataList(url, title, msg);
        });
      });
    });

    btnUpdate.forEach((btn) => {
      btn.addEventListener("click", (e) => {
        e.preventDefault();
        const idCel = e.target
          .closest("tr")
          .querySelector(".idCel").textContent;
        console.log(idCel);
        let urlEnti = e.target.getAttribute("name");
        let urlId = idCel;
        if (urlEnti == "Clientes") {
          urlEnti = "Client";
        }
        if (urlEnti == "Productos") {
          urlEnti = "Product";
        }

        let urlReq = `http://localhost:5226/api/${urlEnti}/${urlId}`;
        if (title == "Flight") {
          urlReq = `http://localhost:5226/api/${urlEnti}/GetDataForUpdate/${urlId}`;
        }
        SendReguest(urlReq, "get", "")
          .then((data) => {
            let attForm = {
              method: "put",
              id: "formEdit",
              title: `Editar ${title}`,
              table: title,
            };
            let dataBtn = {
              id: "btnEdit",
              text: `Edit ${title}`,
              value: urlId,
            };

            /* let urlReqFkData = `http://localhost:5287/ApiVPC/${title}`; */
            if (title == "Flight") {
              let urlReqFkData = CreateUrl(
                "http://localhost:5287/ApiVPC/",
                title
              );
              /* delete data.flightCarries */
              SendReguest(urlReqFkData, "get", "")
                .then((dataFk) => {
                  data["TranspList"] = dataFk;
                  console.log(dataFk);
                  console.log(data);
                  prepararFormData(
                    `http://localhost:5287/ApiVPC/${title}`,
                    attForm,
                    data,
                    `Editar ${title}`,
                    dataBtn
                  );
                })
                .catch((errorFk) => {
                  console.log(errorFk);
                });
              console.log(data);
            } else {
              prepararFormData(
                `http://localhost:5287/ApiVPC/${title}`,
                attForm,
                data,
                `Editar ${title}`,
                dataBtn
              );
            }
          })
          .catch((error) => {
            console.log(error);
          });
      });
    });
  };

  const CreateDataList = (url, title, msg = null, method = "get") => {
    SendReguest(url, method, "")
      .then((dataList) => {
        let table = CreateTable(dataList, title, mainContainer);
        mainPageTitle.textContent = `${title} List`;
        mainContainer.appendChild(TableToContainer(table));

        if (msg !== null && typeof msg === "object") {
          mainContainer.prepend(CreateMsgAlert(msg, "success"));
        }

        LoadActionBtns(url, title);
      })
      .catch((error) => {
        console.error("Error en la consulta: ", error);
      });
  };

  const prepararFormData = (url, formAtt, formData, tableTitle, dataBtn) => {
    mainContainer.innerHTML = "";
    mainPageTitle.textContent = "";

    let formElements = FormElements(formData, formAtt, dataBtn);
    let form = CreateFormCont(formElements, formAtt.title);
    mainContainer.appendChild(form);

    /*  let btnAddEv = formAtt[1] == "post" ? "#btnAdd" : "#btnEdit"; */
    let btnSubmit = document.querySelector(`#${dataBtn.id}`);
    btnSubmit.addEventListener("click", (e) => {
      e.preventDefault();

      let dataF = new FormData(document.querySelector(`#${formAtt.id}`));
      console.log(dataF);
      let dataForm = {};

      dataF.forEach((val, key) => {
        if ((key == "price" || key == "trasportId") && val == "") {
          val = 0;
        }
        dataForm[key] = val;
      });

      console.log(dataForm);
      const dataChecked = CheckData(dataForm);

      if (!dataChecked.status) {
        mainContainer.prepend(CreateMsgAlert(dataChecked.msg));
      } else {
        if (formAtt.method == "put") {
          url += `/${btnSubmit.getAttribute("value")}`;
          console.log(url);
        }
        SendReguest(url, formAtt.method, JSON.stringify(dataForm))
          .then((data) => {
            console.log(data);
            if (data.id || data.nombre || data.idCliente || data.idFactura) {
              CreateDataList(url, formAtt.tabletitle);
            } else {
              let msgErrors, msgAlert;
              console.log(data);
              if (data.errors || data.error) {
                if (data.errors) {
                  msgErrors += `<strong>${data.title}</strong>: `;
                  Object.keys(data.errors).forEach((key) => {
                    let msgError = data.errors[key];
                    msgError.forEach((msg) => {
                      msgErrors += `${msg}\n`;
                    });
                  });
                  msgAlert = CreateMsgAlert(msgErrors);
                }
                if (data.error) {
                  msgAlert = CreateMsgAlert(data.error);
                }
              } else {
                msgAlert = CreateMsgAlert(data);
              }
              mainContainer.prepend(msgAlert);
            }
          })
          .catch((error) => {
            console.error(error);
            let msgAlert = CreateMsgAlert(error);
            mainContainer.prepend(msgAlert);
          });
      }
    });
  };

  btnsNav.forEach((btn) => {
    btn.addEventListener("click", (e) => {
      e.preventDefault();
      let dataForm;
      let attForm = {
        method: "post",
        id: "formAdd",
      };
      let dataBtn = {
        id: "btnAdd",
      };

      switch (e.target.getAttribute("id")) {
        case "listaClientes":
          CreateDataList("http://localhost:5226/api/Client", "Clientes");
          break;
        case "listaFacturas":
          CreateDataList("http://localhost:5226/api/Invoice", "Facturas");
          break;
        case "listaProductos":
          CreateDataList("http://localhost:5226/api/Product", "Productos");
          break;
        case "detalleFact":
          CreateDataList(
            "http://localhost:5226/api/InvoiceDetail",
            "Detalle de Facturas"
          );
          break;
        case "clienteForm":
          dataForm = ["Nombre", "Correo", "NumeroTelefono"];
          Object.assign(attForm, {
            title: "Crear Cliente",
            table: "Cliente",
            tabletitle: "Clientes",
          });
          Object.assign(dataBtn, { text: "Crear Cliente" });
          prepararFormData(
            "http://localhost:5226/api/Client",
            attForm,
            dataForm,
            "Detalle del Cliente",
            dataBtn
          );
          break;
        case "productForm":
          dataForm = ["Nombre", "Precio", "StockDisponible"];
          Object.assign(attForm, {
            title: "Crear Producto",
            table: "Producto",
            tabletitle: "Productos",
          });
          Object.assign(dataBtn, {
            text: "Crear Producto",
          });
          prepararFormData(
            "http://localhost:5226/api/Product",
            attForm,
            dataForm,
            "Detalle del Producto",
            dataBtn
          );
          break;
        case "facturasForm":
          dataForm = ["IdCliente"];
          Object.assign(attForm, {
            title: "Crear Factura",
            table: "Factura",
            tabletitle: "Facturas",
          });
          Object.assign(dataBtn, {
            text: "Crear Factura",
          });
          prepararFormData(
            "http://localhost:5226/api/Invoice",
            attForm,
            dataForm,
            "Detalle de la Factura",
            dataBtn
          );
          break;
        case "detaFactuForm":
          dataForm = ["IdFactura", "IdProducto", "Cantidad", "PrecioUnitario"];
          Object.assign(attForm, {
            title: "Crear DetalleFactura",
            table: "DetalleFactura",
            tabletitle: "Detalle de Facturas",
          });
          Object.assign(dataBtn, {
            text: "Crear DetalleFactura",
          });
          prepararFormData(
            "http://localhost:5226/api/InvoiceDetail",
            attForm,
            dataForm,
            "Datos del Detalle de la Factura",
            dataBtn
          );
          break;
        default:
          break;
      }
    });
  });
});
