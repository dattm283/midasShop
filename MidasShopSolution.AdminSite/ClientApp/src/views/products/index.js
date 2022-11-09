import { useState, useEffect } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";
import { Container, Row, Col, Card, Form, Button, Modal } from "react-bootstrap";
import Sidebar from "../../components/Sidebar/ProductSidebar";
import ReactPaginate from 'react-paginate';
import { UpdateButton } from "./update";

const handleDeleteProduct = (id) => {

    axios
        .delete(`https://localhost:5001/api/Products/${id}`)
        .then(function (response) {
            console.log(response);
            window.location.reload(false);
        })
        .catch(function (error) {
            console.log(error);
        });
    console.log(id)
};



function Items({ currentItems }) {
    return (
        <>
            {currentItems &&
                currentItems.map(product =>
                    <tr key={product.id}>
                        <td>{product.id}</td>
                        <td>{product.name}</td>
                        <td>{product.price}</td>
                        <td>{product.originalPrice}</td>
                        <td>{product.stock}</td>
                        <td>{product.dateCreated}</td>
                        <td><UpdateButton productId={product.id} /> <Button variant="danger" type="button" className="delete-product-btn" onClick={() => handleDeleteProduct(product.id)}><i class="fa-solid fa-trash"></i></Button></td>
                    </tr>
                )}
        </>
    );
}

function PaginatedItems({ itemsPerPage, items }) {
    // Here we use item offsets; we could also use page offsets
    // following the API or data you're working with.
    const [itemOffset, setItemOffset] = useState(0);

    // Simulate fetching items from another resources.
    // (This could be items from props; or items loaded in a local state
    // from an API endpoint with useEffect and useState)
    const endOffset = itemOffset + itemsPerPage;
    console.log(`Loading items from ${itemOffset} to ${endOffset}`);
    const currentItems = items.slice(itemOffset, endOffset);
    const pageCount = Math.ceil(items.length / itemsPerPage);

    // Invoke when user click to request another page.
    const handlePageClick = (event) => {
        const newOffset = (event.selected * itemsPerPage) % items.length;
        console.log(
            `User requested page number ${event.selected}, which is offset ${newOffset}`
        );
        setItemOffset(newOffset);
    };

    return (
        <>
            <Items currentItems={currentItems} />
            <ReactPaginate
                nextLabel="Next>"
                onPageChange={handlePageClick}
                pageRangeDisplayed={3}
                marginPagesDisplayed={2}
                pageCount={pageCount}
                previousLabel="<Previous"
                pageClassName="page-item"
                pageLinkClassName="page-link"
                previousClassName="page-item"
                previousLinkClassName="page-link"
                nextClassName="page-item"
                nextLinkClassName="page-link"
                breakLabel="..."
                breakClassName="page-item"
                breakLinkClassName="page-link"
                containerClassName="pagination"
                activeClassName="active"
                renderOnZeroPageCount={null}
            />
        </>
    );
}
const Products = () => {
    let { categorySlug } = useParams();
    // console.log("slug", categorySlug);
    const [products, setProducts] = useState([]);
    const [productCategory, setProductCategory] = useState([]);
    var path = ''
    if (categorySlug) {
        path = `https://localhost:5001/api/Products/category?CategoryId=${categorySlug}&PageIndex=1&PageSize=100`
    } else {
        path = `https://localhost:5001/api/Products?PageIndex=1&PageSize=100`
    }
    useEffect(() => {
        axios
            .get(path)
            .then((res) => setProducts(categorySlug ? res.data.products.items : res.data.items))
            .then((res) => setProductCategory(categorySlug ? res.data.category : []));
    }, []);
    return (
        <Container fluid>

            <Row>
                <Col xs={2} id="sidebar-wrapper">
                    <Sidebar />
                </Col>
                <Col xs={10} id="page-content-wrapper">
                    <table className='table table-striped' aria-labelledby="tabelLabel">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Name</th>
                                <th>Price</th>
                                <th>Original Price</th>
                                <th>Stock</th>
                                <th>Date created</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <PaginatedItems itemsPerPage={5} items={[...products]} />
                        </tbody>
                    </table>
                </Col>
            </Row>
        </Container>
    );
}

export default Products