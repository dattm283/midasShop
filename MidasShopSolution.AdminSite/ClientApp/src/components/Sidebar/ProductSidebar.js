import React from "react";
import { Nav } from "react-bootstrap";
import { useState, useEffect } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";
import { Link } from 'react-router-dom';

const Sidebar = props => {
    let { categorySlug } = useParams();
    const [categories, setCategories] = useState([]);
    useEffect(() => {
        axios
            .get(`https://localhost:5001/api/categories`)
            .then((res) => setCategories(res.data));
    }, []);
    console.log(categories)
    return (
        <>
            <Nav variant="tabs" className="col-md-12 d-none d-md-block bg-light" defaultActiveKey={"/products/" + categorySlug}>
                <ul className="navbar-nav flex-grow">
                    <Nav.Item> <Nav.Link className="text-dark" href="/products/create">Create</Nav.Link></Nav.Item>
                    <Nav.Item> <Nav.Link className="text-dark" href="/products">All</Nav.Link></Nav.Item>
                    {categories.map(category =>
                        <Nav.Item>
                            <Nav.Link className="text-dark" eventKey={"/products/" + category.id} href={"/products/" + category.id}>{category.name}</Nav.Link>
                        </Nav.Item>
                    )}
                </ul>
            </Nav>
        </>
    );
};
export default Sidebar