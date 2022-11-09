import { useState, useEffect } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";
import { Container, Row, Col, Card, Form, Button, Modal } from "react-bootstrap";
import Sidebar from "../../components/Sidebar/ProductSidebar";
import ReactPaginate from 'react-paginate';
import { UpdateButton } from "./update";

const handleDeleteCategory = (id) => {

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
                currentItems.map(category =>
                    <tr key={category.id}>
                        <td>{category.id}</td>
                        <td>{category.name}</td>
                        <td>{category.parentId}</td>
                        {/* <td><UpdateButton productId={category.id} /> <Button variant="danger" type="button" className="delete-product-btn" onClick={() => handleDeleteProduct(product.id)}><i class="fa-solid fa-trash"></i></Button></td> */}
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
                nextLabel="next >"
                onPageChange={handlePageClick}
                pageRangeDisplayed={3}
                marginPagesDisplayed={2}
                pageCount={pageCount}
                previousLabel="< previous"
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
const Categories = () => {
    // console.log("slug", categorySlug);
    const [category, setCategory] = useState([]);
    useEffect(() => {
        axios
            .get("https://localhost:5001/api/Categories")
            .then((res) => setCategory(res.data));
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
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <PaginatedItems itemsPerPage={5} items={[...category]} />
                        </tbody>
                    </table>
                </Col>
            </Row>
        </Container>
    );
}

export default Categories