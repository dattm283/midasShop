import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { ListUser } from "./views/users/ListUser.js";
import Login from "./views/users/Login";
import Products from "./views/products";
import Categories from "./views/categories";
import CreateForm from "./views/products/create";


const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: '/users',
    element: <ListUser />
  },
  {
    path: '/users/login',
    element: <Login />
  },
  {
    path: '/products/create',
    element: <CreateForm />
  },
  {
    path: '/products/:categorySlug',
    element: <Products />
  },
  {
    path: '/products',
    element: <Products />
  },
  {
    path: '/categories',
    element: <Categories />
  }
];

export default AppRoutes;
