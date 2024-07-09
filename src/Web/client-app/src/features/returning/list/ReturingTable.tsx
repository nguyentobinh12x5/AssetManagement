import IColumnOption from "../../../components/table/interfaces/IColumnOption";
import IPagination from "../../../components/table/interfaces/IPagination";
import ISortState from "../../../components/table/interfaces/ISortState";
import { IPagedModel } from "../../../interfaces/IPagedModel";
import Table from "../../../components/table/Table";
import ButtonIcon from "../../../components/ButtonIcon";
import { CheckLg, XLg } from "react-bootstrap-icons";
import Loading from "../../../components/Loading";
import { formatDate } from "../../../utils/dateUtils";
import { calculateNo, PaginationInfo } from "../../../utils/appUtils";
import TextWithTooltip from "../../../components/table/helper/TextToolTip";
import { IBriefReturning } from "../interfaces/IBriefReturning";
import { ReturningState } from "../constants/returning-state";

const columns: IColumnOption[] = [
  { name: "No.", value: "Id" },
  { name: "Asset Code", value: "assignment.Asset.Code" },
  { name: "Asset Name", value: "assignment.Asset.Name" },
  { name: "Requested by", value: "requestedBy" },
  { name: "Assigned Date", value: "assignment.assignedDate" },
  { name: "Accepted by", value: "acceptedBy" },
  { name: "Returned Date", value: "returnedDate" },
  { name: "State", value: "state" },
];

type ReturningTableProps = {
  returnings: IPagedModel<IBriefReturning>;
  handleSort: (value: string) => void;
  handlePaging: (page: number) => void;
  sortState: ISortState;
  searchTerm: string;
  sortColumnDirection: string;
};

const ReturningTable: React.FC<ReturningTableProps> = ({
  returnings,
  sortState,
  searchTerm,
  handleSort,
  handlePaging,
  sortColumnDirection,
}) => {
  const { items, pageNumber, totalPages, totalCount } = returnings;

  const pagination: IPagination = {
    currentPage: pageNumber,
    totalPage: totalPages,
    handleChange: handlePaging,
  };

  if (!returnings) {
    return <Loading />;
  }

  if (items?.length === 0 && searchTerm) {
    return (
      <div className="text-center">
        <p>There's no data, please adjust your search condition</p>
      </div>
    );
  }

  if (items?.length === 0) {
    return (
      <div className="text-center">
        <p>No data available</p>
      </div>
    );
  }

  return (
    <>
      <Table
        columns={columns}
        sortState={sortState}
        handleSort={handleSort}
        pagination={pagination}
      >
        {items?.map((data, index) => {
          const paginationInfo: PaginationInfo = {
            pageNumber: pageNumber,
            pageSize: 5,
            totalCount: totalCount,
            sortDirection: sortColumnDirection,
          };
          return (
            <tr key={data.id}>
              <td>{calculateNo(index, paginationInfo)}</td>
              <td>
                <TextWithTooltip text={data.assetCode} />
              </td>
              <td>
                <TextWithTooltip text={data.assetName} />
              </td>
              <td>
                <TextWithTooltip text={data.requestedBy} />
              </td>
              <td>
                <TextWithTooltip text={formatDate(data.assignedDate)} />
              </td>
              <td>
                <TextWithTooltip text={data.acceptedBy} />
              </td>
              <td>
                <TextWithTooltip text={formatDate(data.returnedDate)} />
              </td>
              <td>
                <TextWithTooltip text={ReturningState[data.state]} />
              </td>
              <td className="action" onClick={(e) => e.stopPropagation()}>
                <div className="d-flex gap-3 justify-content-evenly align-items-center">
                  <ButtonIcon>
                    <CheckLg
                      color="#cf2338"
                      stroke="#cf2338"
                      strokeWidth={1.5}
                      size={20}
                    />
                  </ButtonIcon>
                  <ButtonIcon>
                    <XLg
                      color="gray"
                      stroke="gray"
                      strokeWidth={1.5}
                      size={20}
                    />
                  </ButtonIcon>
                </div>
              </td>
            </tr>
          );
        })}
      </Table>
    </>
  );
};

export default ReturningTable;
