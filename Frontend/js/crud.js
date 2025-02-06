const SendReguest = async (url, method, dataForm) => {
  const confgFetch = {
    method: method,
    headers: { "content-Type": "application/json" },
  };

  (method == "post" || method == "put") && (confgFetch.body = dataForm);

  if (method == "delete") {
    let peticion = await fetch(url, confgFetch);
    return peticion;
  }

  let peticion = await (await fetch(url, confgFetch)).json();
  return peticion;
};

const CreateUrl = (url, enti) => {
  enti == "Flight" && (url += `Transport`);
  return url;
};

export { SendReguest, CreateUrl };
