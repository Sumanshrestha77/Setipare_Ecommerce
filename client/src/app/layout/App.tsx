import { useEffect, useState } from "react";
import { ListFormat } from "typescript";
import { Product } from '../models/product';
import Catalog from "../../features/catalog/catalog";

// const products =[
//   {name: 'product1',price:100.00},
//   {name: 'product2',price:100.00},
//   {name: 'product3',price:100.00},
// ]
function App() {
  const [products, setProducts] = useState<Product[]>([]);
  useEffect(()=>{
    fetch('http://localhost:5000/api/Products')
    .then(response => response.json())
    .then(data => setProducts(data))
  }, []) //[]empthy dependency, only 1 time is run, else every time compent is run this products is renders
  function addProduct(){ //spread operator ... new array ma, 
    setProducts(prevState =>[...prevState, 
      {
        id: prevState.length+101,
        name: 'product4'+(prevState.length+1), 
        price:(prevState.length*100)+100,
        brand:'Setipare',
        description:'shirt',
        pictureUrl:'http://picsum.photos/200'
      }])
  }
  
  return (
    <div className="App">
     <h1>Setipare Website on the way</h1>
     <Catalog products={products} addProduct={addProduct}/>  
       
    </div> 
  );
}

export default App;
