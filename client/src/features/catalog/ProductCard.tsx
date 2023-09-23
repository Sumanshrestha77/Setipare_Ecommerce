import { ListItem, ListItemAvatar, Avatar, ListItemText } from "@mui/material";
import { Product } from "../../app/models/product";

interface Props{
    product: Product;
}


export default function ProductCard({product}:Props){
    return(
        <ListItem key={product.name}>
        <ListItemAvatar>
            <Avatar src={product.pictureUrl}/>
            <ListItemText>
                {product.name} - {product.price}
            </ListItemText>
             
        </ListItemAvatar>

      </ListItem>
    )
}