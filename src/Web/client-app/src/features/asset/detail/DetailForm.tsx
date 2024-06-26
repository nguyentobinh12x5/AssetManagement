import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import ConfirmModal from "../../../components/confirmModal/ConfirmModal";
import ButtonIcon from "../../../components/ButtonIcon";
import { XCircle } from "react-bootstrap-icons";
import { getAssetById } from "../reducers/asset-detail-slice";
import { RootState } from "../../../redux/store";
import { Col, Row } from "react-bootstrap";
import { text } from "stream/consumers";
import Table from "../../../components/table/Table";
import IColumnOption from "../../../components/table/interfaces/IColumnOption";
import { formatDate } from "../../../utils/dateUtils";

type AssetID = {
  id: string;
  onClose: () => void;
};

const DetailForm: React.FC<AssetID> = ({ id, onClose }) => {
  const [isModalOpen, setIsModalOpen] = useState(true);
  const dispatch = useDispatch();
  const { assetDetail } = useSelector((state: RootState) => state.assetDetail);

  const columns: IColumnOption[] = [
    { name: "Date", value: "Date", disable: true },
    { name: "Assigned to", value: "AssignedTo", disable: true },
    { name: "Assigned by", value: "AssignedBy", disable: true },
    { name: "Returned Date", value: "ReturnedDate", disable: true },
  ];

  useEffect(() => {
    if (isModalOpen) {
      dispatch(getAssetById(id));
    }
    console.log(assetDetail);
  }, [isModalOpen, dispatch, id]);

  const handleCloseModal = () => {
    setIsModalOpen(false);
    onClose();
  };

  return (
    <div className="container m-auto p-5">
      <ConfirmModal
        title="Detailed Asset Information"
        isShow={isModalOpen}
        onHide={handleCloseModal}
        dialogClassName="modal-150w modal-detail"
        isShowClose={true}
      >
        <div className="form-detail">
          <div>
            <Row className="mb-3">
              <Col md={3}>Asset Code</Col>
              <Col md={9}>{assetDetail?.code}</Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Asset Name</Col>
              <Col md={9}>{assetDetail?.name}</Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Category</Col>
              <Col md={9}>{assetDetail?.categoryName}</Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Installed Date</Col>
              <Col md={9}>
                {formatDate(
                  assetDetail
                    ? assetDetail.installedDate
                    : new Date().toDateString()
                )}
              </Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>State</Col>
              <Col md={9}>{assetDetail?.assetStatusName}</Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Location</Col>
              <Col md={9}>{assetDetail?.location}</Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Specification</Col>
              <Col md={5}>{assetDetail?.specification}</Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col
                md={3}
                style={{
                  paddingTop: 8,
                }}
              >
                History
              </Col>
              <Col className="p-1" md={9}>
                <Table
                  columns={columns}
                  sortState={{
                    name: "",
                    orderBy: "",
                  }}
                  handleSort={() => {}}
                  pagination={{
                    currentPage: 0,
                    totalPage: 0,
                    handleChange: () => {},
                  }}
                >
                  <tr>
                    <td className="smlsize">12/10/2018</td>
                    <td className="smlsize">hungtv1</td>
                    <td className="smlsize">binhnv</td>
                    <td className="smlsize">07/03/2019</td>
                  </tr>
                  <tr>
                    <td className="smlsize">10/03/2024</td>
                    <td className="smlsize">thinhptx</td>
                    <td className="smlsize">tuanha</td>
                    <td className="smlsize">22/03/2024</td>
                  </tr>
                </Table>
              </Col>
            </Row>
          </div>
        </div>
      </ConfirmModal>
    </div>
  );
};

export default DetailForm;
